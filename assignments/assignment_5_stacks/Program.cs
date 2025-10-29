using System;

namespace Assignment5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🌐 Browser Navigation System");
            Console.WriteLine("============================");
            Console.WriteLine();

            var navigator = new BrowserNavigator();
            navigator.Run();
        }
    }
}