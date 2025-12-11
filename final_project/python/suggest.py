#!/usr/bin/env python3
import sys, json, math, re
from typing import Dict, List, Tuple

# --- Category keyword dictionary (expanded) ---
CATEGORIES: Dict[str, List[str]] = {
    "income": ["paycheck", "salary", "deposit", "refund", "bonus", "income", "venmo received", "cashapp received", "zelle"],
    "takeout": ["mcdonald", "burger king", "popeyes", "chipotle", "wendy's", "wendys", "kfc", "takeout", "drive-thru", "drivethru", "fast food", "doordash", "uber eats", "ubereats", "grubhub", "in-n-out", "innout"],
    "coffee": ["starbucks", "dunkin", "latte", "espresso", "cafe", "mocha"],
    "groceries": ["safeway", "kroger", "trader joe", "trader joe's", "grocery", "market", "aldi", "whole foods", "walmart", "costco", "heb", "food lion"],
    "transport": ["uber", "lyft", "gas", "shell", "chevron", "metro", "bus", "train", "parking", "amtrak", "greyhound"],
    "rent": ["rent", "landlord", "apartment", "lease"],
    "restaurants": ["restaurant", "grill", "bar", "pizza", "steakhouse", "buffet", "bistro", "diner"],
    "subscriptions": ["netflix", "spotify", "icloud", "prime", "hulu", "max", "crunchyroll", "apple music", "youtube premium", "xbox live", "playstation", "patreon"],
    "utilities": ["electric", "water", "sewer", "gas bill", "utility", "pg&e", "pge", "comcast", "xfinity", "internet", "wifi", "att", "verizon", "t-mobile"],
    "healthcare": ["pharmacy", "walgreens", "cvs", "co-pay", "copay", "doctor", "hospital", "urgent care", "clinic", "rx", "dental", "vision"],
    "shopping": ["amazon", "target", "mall", "shoes", "clothes", "apparel", "electronics", "best buy", "ikea"],
    "insurance": ["insurance", "geico", "state farm", "allstate", "progressive", "premium", "policy"],
    "education": ["tuition", "books", "school", "course", "class", "registration", "exam", "fees"],
    "travel": ["hotel", "airbnb", "flight", "plane", "delta", "united", "airport", "rental car", "lyft"],
    "bills": ["bill", "statement", "invoice", "payment due", "autopay"],
}

# --- Phrase rules (exact or near-exact multiword phrases) with category + weight ---
PHRASES: List[Tuple[str, str, float]] = [
    ("in-n-out", "takeout", 3.0),
    ("taco bell", "takeout", 2.5),
    ("shell gas", "transport", 2.5),
    ("shell fuel", "transport", 2.5),
    ("whole foods", "groceries", 3.0),
    ("trader joe", "groceries", 3.0),
    ("starbucks", "coffee", 3.0),
    ("rent payment", "rent", 5.0),
    ("security deposit", "rent", 3.0),
    ("spotify", "subscriptions", 3.0),
    ("youtube premium", "subscriptions", 3.0),
    ("apple music", "subscriptions", 3.0),
]

DEFAULT_CATEGORY = "(uncategorized)"

# --- Keyword weights: some terms are stronger signals ---
KEYWORD_BONUS: Dict[str, float] = {
    "paycheck": 4.0, "salary": 4.0, "deposit": 3.5, "refund": 3.0, "bonus": 3.5,
    "rent": 5.0, "landlord": 4.0, "apartment": 3.5,
    "netflix": 3.0, "spotify": 3.0, "amazon": 2.0, "uber": 2.0, "lyft": 2.0,
    "starbucks": 3.5, "gas": 2.5, "shell": 2.5, "chevron": 2.5,
}

# values are (min_inclusive, max_inclusive, {category: weight})
AMOUNT_RULES: List[Tuple[float, float, Dict[str, float]]] = [
    (1.0, 10.0, {"coffee": 2.0, "takeout": 1.0, "subscriptions": 0.5}),
    (10.01, 30.0, {"takeout": 2.5, "transport": 1.0}),
    (30.01, 120.0, {"groceries": 2.0, "transport": 1.0, "restaurants": 1.0}),
    (120.01, 400.0, {"groceries": 1.5, "utilities": 1.5, "shopping": 1.5}),
    (400.01, 2000.0, {"rent": 3.5, "education": 1.0, "insurance": 1.0, "utilities": 1.0}),
]

# if amount > 0, strongly push to income
INCOME_AMOUNT_BONUS = 4.0

TOKEN_SPLIT = re.compile(r"[\s,.;:()\[\]\-_/]+")

def tokenize(text: str) -> List[str]:
    return [t for t in TOKEN_SPLIT.split(text.lower()) if t]


def score_text(tokens: List[str]) -> Dict[str, float]:
    scores: Dict[str, float] = {c: 0.0 for c in CATEGORIES}
    # simple substring token match with bonuses
    for cat, kws in CATEGORIES.items():
        for kw in kws:
            k = kw.lower()
            # keyword present in any token
            if any(k in tok for tok in tokens):
                scores[cat] += 1.0 + KEYWORD_BONUS.get(k, 0.0)
    # phrases
    joined = " ".join(tokens)
    for phrase, cat, w in PHRASES:
        if phrase in joined:
            scores[cat] += w
    return scores


def score_amount(amount: float) -> Dict[str, float]:
    scores: Dict[str, float] = {c: 0.0 for c in CATEGORIES}
    if amount > 0:
        scores["income"] += INCOME_AMOUNT_BONUS
    a = abs(amount)
    for low, high, boosts in AMOUNT_RULES:
        if low <= a <= high:
            for cat, w in boosts.items():
                scores[cat] += w
    return scores


def combine_scores(*parts: Dict[str, float]) -> Dict[str, float]:
    total: Dict[str, float] = {c: 0.0 for c in CATEGORIES}
    for p in parts:
        for c, v in p.items():
            total[c] += v
    return total


def pick_category(scores: Dict[str, float]) -> Tuple[str, float]:
    # choose best; compute confidence from margin vs runner-up using softmax-like ratio
    sorted_items = sorted(scores.items(), key=lambda x: x[1], reverse=True)
    top_cat, top_score = sorted_items[0]
    if top_score <= 0:
        return DEFAULT_CATEGORY, 0.0
    second_score = sorted_items[1][1] if len(sorted_items) > 1 else 0.0
    # confidence: top / (top + second + 5) to be conservative
    conf = top_score / (top_score + max(second_score, 0.0) + 5.0)
    conf = max(0.0, min(1.0, conf))
    return top_cat, conf


def classify(text: str, amount: float) -> Tuple[str, float]:
    tokens = tokenize(text or "")
    s_text = score_text(tokens)
    s_amt = score_amount(float(amount))
    scores = combine_scores(s_text, s_amt)
    return pick_category(scores)


def suggest_one(text: str, amount: float):
    cat, conf = classify(text, amount)
    return {"category": cat, "confidence": round(conf, 2)}


def handle_suggest_category(req):
    text = req.get("text", "")
    amount = float(req.get("amount", 0))
    return {"ok": True, "result": suggest_one(text, amount)}


def handle_bulk_suggest(req):
    txs = req.get("transactions", [])
    results = []
    for t in txs:
        tid = t.get("id")
        text = t.get("description", "")
        amt = float(t.get("amount", 0))
        res = suggest_one(text, amt)
        results.append({"id": tid, **res})
    return {"ok": True, "results": results}


def main():
    try:
        raw = sys.stdin.read()
        req = json.loads(raw or "{}")
        op = req.get("op")
        if op == "suggest_category":
            out = handle_suggest_category(req)
        elif op == "bulk_suggest":
            out = handle_bulk_suggest(req)
        else:
            raise ValueError(f"unsupported op: {op}")
        print(json.dumps(out))
        sys.exit(0)
    except Exception as e:
        print(json.dumps({"ok": False, "error": str(e)}))
        sys.exit(1)

if __name__ == "__main__":
    main()