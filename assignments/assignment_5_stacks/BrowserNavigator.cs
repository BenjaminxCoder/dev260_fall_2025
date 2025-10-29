using System;

namespace Assignment5
{
    /// <summary>
    /// Handles user interface and interaction for the browser navigation system
    /// </summary>
    public class BrowserNavigator
    {
        private BrowserSession session;

        public BrowserNavigator()
        {
            session = new BrowserSession();
        }

        /// <summary>
        /// Main application loop
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Welcome to the Browser Navigation System!");
            Console.WriteLine("This simulates how web browsers handle back/forward navigation.\n");

            bool running = true;
            while (running)
            {
                DisplayCurrentPage();
                DisplayMenu();
                
                string choice = Console.ReadLine()?.ToLower().Trim() ?? "";
                Console.WriteLine(); // Add spacing

                switch (choice)
                {
                    case "1":
                    case "visit":
                        HandleVisitUrl();
                        break;
                    case "2":
                    case "back":
                        HandleGoBack();
                        break;
                    case "3":
                    case "forward":
                        HandleGoForward();
                        break;
                    case "4":
                    case "history":
                        HandleShowHistory();
                        break;
                    case "5":
                    case "status":
                        HandleShowStatus();
                        break;
                    case "6":
                    case "clear":
                        HandleClearHistory();
                        break;
                    case "7":
                    case "exit":
                        running = false;
                        ShowGoodbye();
                        break;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        /// <summary>
        /// Display current page information
        /// </summary>
        private void DisplayCurrentPage()
        {
            Console.WriteLine("┌─ Current Page ─────────────────────────────────┐");
            if (session.CurrentPage == null)
            {
                Console.WriteLine("│ 🏠 No page loaded - Start by visiting a URL   │");
            }
            else
            {
                Console.WriteLine($"│ {session.CurrentPage.ToDisplayString().Replace("\n", " │\n│ ")} │");
            }
            Console.WriteLine("└───────────────────────────────────────────────┘");
        }

        /// <summary>
        /// Display navigation menu
        /// </summary>
        private void DisplayMenu()
        {
            string backStatus = session.CanGoBack ? "✅" : "❌";
            string forwardStatus = session.CanGoForward ? "✅" : "❌";

            Console.WriteLine("┌─ Navigation Menu ──────────────────────────────┐");
            Console.WriteLine("│ 1. Visit URL     │ 2. Back " + backStatus + "     │ 3. Forward " + forwardStatus + " │");
            Console.WriteLine("│ 4. Show History  │ 5. Status      │ 6. Clear       │");
            Console.WriteLine("│ 7. Exit          │                │                │");
            Console.WriteLine("└────────────────────────────────────────────────┘");
            Console.Write("Choose option (number or name): ");
        }

        /// <summary>
        /// Handle visiting a new URL
        /// TODO: Students need to implement the session.VisitUrl() method
        /// </summary>
        private void HandleVisitUrl()
        {
            Console.Write("Enter URL: ");
            string? url = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrWhiteSpace(url))
            {
                Console.WriteLine("❌ URL cannot be empty.\n");
                return;
            }

            // Basic URL validation
            if (!IsValidUrl(url))
            {
                Console.WriteLine("❌ Please enter a valid URL (must start with http:// or https://).\n");
                return;
            }

            Console.Write("Enter page title: ");
            string? title = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrWhiteSpace(title))
            {
                title = ExtractTitleFromUrl(url);
            }

            try
            {
                session.VisitUrl(url, title);
                Console.WriteLine($"✅ Navigated to: {title}\n");
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  VisitUrl method not yet implemented in BrowserSession class.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error visiting URL: {ex.Message}\n");
            }
        }

        /// <summary>
        /// Handle going back in navigation
        /// TODO: Students need to implement the session.GoBack() method
        /// </summary>
        private void HandleGoBack()
        {
            if (!session.CanGoBack)
            {
                Console.WriteLine("❌ Cannot go back - no pages in back history.\n");
                return;
            }

            try
            {
                bool success = session.GoBack();
                if (success)
                {
                    Console.WriteLine($"⬅️  Went back to: {session.CurrentPage?.Title}\n");
                }
                else
                {
                    Console.WriteLine("❌ Unable to go back.\n");
                }
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  GoBack method not yet implemented in BrowserSession class.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error going back: {ex.Message}\n");
            }
        }

        /// <summary>
        /// Handle going forward in navigation
        /// TODO: Students need to implement the session.GoForward() method
        /// </summary>
        private void HandleGoForward()
        {
            if (!session.CanGoForward)
            {
                Console.WriteLine("❌ Cannot go forward - no pages in forward history.\n");
                return;
            }

            try
            {
                bool success = session.GoForward();
                if (success)
                {
                    Console.WriteLine($"➡️  Went forward to: {session.CurrentPage?.Title}\n");
                }
                else
                {
                    Console.WriteLine("❌ Unable to go forward.\n");
                }
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  GoForward method not yet implemented in BrowserSession class.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error going forward: {ex.Message}\n");
            }
        }

        /// <summary>
        /// Display navigation history
        /// </summary>
        private void HandleShowHistory()
        {
            Console.WriteLine("📚 Navigation History");
            Console.WriteLine("═══════════════════");
            
            try
            {
                session.DisplayBackHistory();
                Console.WriteLine();
                session.DisplayForwardHistory();
                Console.WriteLine();
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("⚠️  DisplayBackHistory and DisplayForwardHistory methods not yet implemented in BrowserSession class.\n");
            }
        }

        /// <summary>
        /// Display navigation status
        /// </summary>
        private void HandleShowStatus()
        {
            Console.WriteLine(session.GetNavigationStatus());
            Console.WriteLine();
        }

        /// <summary>
        /// Handle clearing navigation history
        /// </summary>
        private void HandleClearHistory()
        {
            if (session.BackHistoryCount == 0 && session.ForwardHistoryCount == 0)
            {
                Console.WriteLine("ℹ️  Navigation history is already empty.\n");
                return;
            }

            Console.Write($"Are you sure you want to clear all navigation history? (y/N): ");
            string? confirmation = Console.ReadLine()?.ToLower().Trim();
            
            if (confirmation == "y" || confirmation == "yes")
            {
                try
                {
                    session.ClearHistory();
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("⚠️  ClearHistory method not yet implemented in BrowserSession class.");
                }
            }
            else
            {
                Console.WriteLine("❌ Clear operation cancelled.");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Display goodbye message
        /// </summary>
        private void ShowGoodbye()
        {
            Console.WriteLine("═══════════════════════════════════════");
            Console.WriteLine("🌐 Browser Navigation Session Summary");
            Console.WriteLine("═══════════════════════════════════════");
            
            if (session.CurrentPage != null)
            {
                Console.WriteLine($"Final page: {session.CurrentPage.Title}");
            }
            
            Console.WriteLine(session.GetNavigationStatus());
            Console.WriteLine();
            Console.WriteLine("Thanks for using Browser Navigation System!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Basic URL validation
        /// </summary>
        private bool IsValidUrl(string url)
        {
            return url.StartsWith("http://") || url.StartsWith("https://");
        }

        /// <summary>
        /// Extract a title from URL if none provided
        /// </summary>
        private string ExtractTitleFromUrl(string url)
        {
            try
            {
                var uri = new Uri(url);
                return uri.Host;
            }
            catch
            {
                return "Untitled Page";
            }
        }
    }
}