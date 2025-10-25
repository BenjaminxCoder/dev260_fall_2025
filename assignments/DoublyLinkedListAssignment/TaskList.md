# ✅ Task Checklist (Outline-Ordered)

## 0) Assignment Focus & Setup
- [x] Use the starter project in **Downloads/assignment_3_doubly_linked_lists**.
- [x] Create work branch:  
  `git checkout -b week-3-doubly-linked-list`
- [x] Create repo path: `assignments/week-3-doubly-linked-list/` and place all project files there.
- [x] Build and run baseline:  
  `dotnet build` → should succeed.  
  `dotnet run` → placeholder output ok.
- [x] Commit scaffold:  
  `git add .`  
  `git commit -m "Week 3: scaffold DoublyLinkedList assignment"`

## 1) 📚 Learning Objectives
- [x] Review objectives and references in README.
- [x] Skim linked docs for generics, pointers (Prev/Next), and traversal patterns.

## 2) 📋 Part A — Core Implementation (60 pts)

**Step 1–2: Node + List foundation**
- [x] Implement `Node<T>` with `Data`, `Next`, `Previous`.
- [x] Implement `DoublyLinkedList<T>` with fields `head`, `tail`, `count`; props `Count`, `IsEmpty`.
- [x] Smoke test: create empty list, assert `Count==0`, `IsEmpty==true`.

**Step 3: Addition operations**
- [x] `AddFirst(T item)` handles empty and non-empty.
- [x] `AddLast(T item)` handles empty and non-empty. Add `Add(T)` → `AddLast`.
- [x] `Insert(int index, T item)` supports `0`, `Count`, and middle inserts. Validate indices.
- [x] Tests: add sequences to front/end; insert at 0, mid, Count; verify head/tail links and `Count`.

**Step 4: Traversal & display**
- [x] `PrintForward()` from head→tail.
- [x] `PrintBackward()` from tail→head.
- [x] `ToArray()` for test assertions.
- [x] Tests: forward and backward produce same multiset, reversed order respectively.

**Step 5: Search**
- [x] `Contains(T item)`.
- [x] `Find(T item)` returns node or `null`.
- [x] `IndexOf(T item)` returns index or `-1`.
- [x] Tests: hits and misses; duplicates return first index.

**Step 6: Removal**
- [x] `RemoveFirst()` handles empty, single, multi.
- [x] `RemoveLast()` handles empty, single, multi.
- [x] `Remove(T item)` removes first occurrence; fix links; decrements `count`.
- [x] `RemoveAt(int index)` validates bounds; unlinks node.
- [x] Tests: remove on empty, single, head/mid/tail; verify head/tail integrity.

**Step 7: Advanced (bonus)**
- [x] `Reverse()` in-place by swapping `Next`/`Previous`; swap head/tail.
- [x] `Clear()` severs all links; sets `head=tail=null`, `count=0`.
- [x] Tests: reverse odd/even lengths; clear from non-empty.

## 3) 🎵 Part B — Music Playlist Manager (40 pts)

**Step 8: Song class**
- [x] Implement `Song` with `Title`, `Artist`, `Year`, `Duration`; ctor; `ToString()`.

**Step 9: Playlist core**
- [x] Create `MusicPlaylist` with `DoublyLinkedList<Song> playlist` and `Node<Song>? currentSong`.
- [x] Props: `TotalSongs`, `CurrentSong`, `HasSongs`.

**Step 10: Management**
- [x] `AddSong(Song)`, `AddSongAt(int, Song)` maintain current song.
- [x] `RemoveSong(Song)`, `RemoveSongAt(int)` update current if needed.
- [x] Navigation: `Next()`, `Previous()`, `JumpToSong(int)`.

**Step 11: UI / Manager**
- [ ] Implement menu in `MusicPlaylistManager.cs`: show forward/backward, play next/prev, add/remove/jump/clear.
- [ ] Highlight current track in listing (`►` prefix).
- [ ] Input validation and prompts.

## 4) 🔌 Program wiring
- [ ] In `Program.cs`, add menu to run:  
  1) Core list demo  
  2) Music playlist manager
- [ ] Ensure both flows work.

## 5) 🧪 Testing & Evidence
- [ ] Add smoke tests in `CoreListDemo.cs`.
- [ ] Cover edge cases: empty list, single element, invalid indices, duplicates.
- [ ] Verify bidirectional consistency after each mutation.

## 6) 📝 Documentation
- [ ] Add `STUDY_NOTES.md` with setup/run steps.
- [ ] Add step log, challenges, and performance reflection.

## 7) ✅ Code Quality & Integrity
- [ ] No built-in collections.
- [ ] Comment pointer logic.
- [ ] Project builds cleanly.
- [ ] Attribute any references.

## 8) 🚀 Submission
- [ ] Commit and push branch:  
  `git add .`  
  `git commit -m "Week 3: complete DLL + playlist"`  
  `git push origin week-3-doubly-linked-list`
- [ ] Open PR → merge → verify `main` runs.
- [ ] Submit GitHub repo link as deliverable.