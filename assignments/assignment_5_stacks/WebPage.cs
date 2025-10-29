using System;

namespace Assignment5
{
    /// <summary>
    /// Represents a web page with URL, title, and visit timestamp
    /// </summary>
    public class WebPage
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime VisitTime { get; set; }

        public WebPage(string url, string title)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            VisitTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Title} ({Url}) - Visited: {VisitTime:yyyy-MM-dd HH:mm:ss}";
        }

        /// <summary>
        /// Creates a simple display format for current page
        /// </summary>
        public string ToDisplayString()
        {
            return $"🌐 {Title}\n   📍 {Url}\n   🕒 {VisitTime:MMM dd, yyyy 'at' h:mm tt}";
        }
    }
}