using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvancedCSharp03
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Exercise 1: Student Grade Manager (List<int>)
            Console.WriteLine("==================================================");
            Console.WriteLine("=== Exercise 1: Student Grade Manager (List) ===");
            Console.WriteLine("==================================================");

            List<int> grades = new List<int> { 85, 92, 78, 95, 88, 70, 100, 65 };
            Console.WriteLine($"Grades: {string.Join(", ", grades)}");
            Console.WriteLine($"Count: {grades.Count}, First: {grades[0]}, Last: {grades[grades.Count - 1]}");

            grades.Sort();
            Console.WriteLine($"Sorted Ascending: {string.Join(", ", grades)}");

            int firstAbove90 = grades.Find(g => g > 90);
            Console.WriteLine($"First grade above 90: {firstAbove90}");

            List<int> failingGrades = grades.FindAll(g => g < 75);
            Console.WriteLine($"Failing grades (< 75): {string.Join(", ", failingGrades)}");

            grades.RemoveAll(g => g < 75);
            Console.WriteLine($"After removing failing grades: {string.Join(", ", grades)}");

            bool has100 = grades.Contains(100);
            Console.WriteLine($"Contains grade 100? {has100}");

            List<string> formattedGrades = grades.ConvertAll(g => $"Grade: {g}");
            Console.WriteLine($"Formatted List: {string.Join(" | ", formattedGrades)}");
            #endregion

            #region Exercise 2: Leaderboard (SortedDictionary)
            Console.WriteLine("\n==================================================");
            Console.WriteLine("=== Exercise 2: Leaderboard (SortedDictionary) ===");
            Console.WriteLine("==================================================");

            SortedDictionary<int, string> leaderboard = new SortedDictionary<int, string>()
            {
                { 500, "Ahmed" },
                { 200, "Sara" },
                { 800, "Ali" },
                { 350, "Mona" }
            };

            Console.WriteLine("Leaderboard entries (automatically sorted by score):");
            foreach (var kvp in leaderboard)
            {
                Console.WriteLine($"Score: {kvp.Key} -> Player: {kvp.Value}");
            }

            var firstEntry = leaderboard.First();
            Console.WriteLine($"Lowest Score Key: {firstEntry.Key}, Player: {firstEntry.Value}");

            bool has500 = leaderboard.ContainsKey(500);
            Console.WriteLine($"Score 500 exists? {has500}");

            if (leaderboard.TryGetValue(999, out string player999))
                Console.WriteLine($"Player with score 999: {player999}");
            else
                Console.WriteLine("Player with score 999: Not Found");

            leaderboard.Remove(200);
            Console.WriteLine("After removing score 200:");
            foreach (var kvp in leaderboard)
            {
                Console.WriteLine($"Score: {kvp.Key} -> Player: {kvp.Value}");
            }
            #endregion

            #region Exercise 3: Phone Book (Dictionary)
            Console.WriteLine("\n==================================================");
            Console.WriteLine("=== Exercise 3: Phone Book (Dictionary) ===");
            Console.WriteLine("==================================================");

            Dictionary<string, string> phoneBook = new Dictionary<string, string>()
            {
                { "Ahmed", "01011112222" },
                { "Sara", "01133334444" },
                { "Ali", "01255556666" },
                { "Mona", "01577778888" }
            };

            // Add or update with [] syntax
            phoneBook["Hossam"] = "01099990000";
            Console.WriteLine("Added Hossam using indexer.");

            // Try adding duplicate with .Add()
            try
            {
                phoneBook.Add("Ahmed", "01000000000");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Caught expected exception on duplicate .Add(): {ex.Message}");
            }

            // Try adding duplicate with .TryAdd()
            bool addedSara = phoneBook.TryAdd("Sara", "01100000000");
            Console.WriteLine($"TryAdd duplicate 'Sara' succeeded? {addedSara}");

            // Search non-existent contact with fallback
            if (phoneBook.TryGetValue("Omar", out string omarPhone))
                Console.WriteLine($"Omar: {omarPhone}");
            else
                Console.WriteLine("Omar: Not Found");

            Console.WriteLine($"All Names (Keys): {string.Join(", ", phoneBook.Keys)}");
            Console.WriteLine($"All Phones (Values): {string.Join(", ", phoneBook.Values)}");
            #endregion

            #region Exercise 4: Unique Email Validator (HashSet)
            Console.WriteLine("\n==================================================");
            Console.WriteLine("=== Exercise 4: Unique Email Validator (HashSet) ===");
            Console.WriteLine("==================================================");

            HashSet<string> emailSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            emailSet.Add("ahmed@test.com");
            emailSet.Add("AHMED@test.com");
            emailSet.Add("sara@test.com");
            emailSet.Add("Sara@Test.Com");

            Console.WriteLine($"Total Unique Emails stored: {emailSet.Count}");
            Console.WriteLine("Explanation: HashSet enforces uniqueness; with StringComparer.OrdinalIgnoreCase, case differences are treated as duplicates.");
            Console.WriteLine($"Stored Emails: {string.Join(", ", emailSet)}");

            HashSet<int> setA = new HashSet<int> { 1, 2, 3, 4, 5 };
            HashSet<int> setB = new HashSet<int> { 4, 5, 6, 7, 8 };

            HashSet<int> union = new HashSet<int>(setA);
            union.UnionWith(setB);
            Console.WriteLine($"Union: {string.Join(", ", union)}");

            HashSet<int> intersect = new HashSet<int>(setA);
            intersect.IntersectWith(setB);
            Console.WriteLine($"Intersect: {string.Join(", ", intersect)}");

            HashSet<int> except = new HashSet<int>(setA);
            except.ExceptWith(setB);
            Console.WriteLine($"Except (A - B): {string.Join(", ", except)}");

            HashSet<int> sub = new HashSet<int> { 1, 2 };
            Console.WriteLine($"Is {{1, 2}} a subset of Set A? {sub.IsSubsetOf(setA)}");
            #endregion

            #region Exercise 5: Print Queue Simulator (Queue<string>)
            Console.WriteLine("\n==================================================");
            Console.WriteLine("=== Exercise 5: Print Queue Simulator (Queue) ===");
            Console.WriteLine("==================================================");

            Queue<string> printQueue = new Queue<string>();
            string[] docs = { "Report.pdf", "Invoice.pdf", "Letter.docx", "Resume.pdf", "Photo.jpg" };
            foreach (var doc in docs) printQueue.Enqueue(doc);

            Console.WriteLine($"Queue items: {string.Join(", ", printQueue)} (Count: {printQueue.Count})");
            Console.WriteLine($"Next document to print (Peek): {printQueue.Peek()}");

            while (printQueue.Count > 0)
            {
                Console.WriteLine($"Printing: {printQueue.Dequeue()}");
            }

            bool dequeuedEmpty = printQueue.TryDequeue(out string emptyResult);
            Console.WriteLine($"TryDequeue on empty queue returned: {dequeuedEmpty} (Result: '{emptyResult}')");
            #endregion

            #region Exercise 6: Browser History (Stack<string>)
            Console.WriteLine("\n==================================================");
            Console.WriteLine("=== Exercise 6: Browser History (Stack) ===");
            Console.WriteLine("==================================================");

            Stack<string> browserHistory = new Stack<string>();
            string[] urls = { "google.com", "github.com", "stackoverflow.com", "youtube.com", "claude.ai" };
            foreach (var url in urls) browserHistory.Push(url);

            Console.WriteLine($"Current active page (Peek): {browserHistory.Peek()}");

            Console.WriteLine("Pressing 'back' 3 times:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Leaving: {browserHistory.Pop()}");
            }

            Console.WriteLine($"Current page after going back: {browserHistory.Peek()}");

            // Empty the stack
            while (browserHistory.Count > 0) browserHistory.Pop();

            bool poppedEmpty = browserHistory.TryPop(out string emptyUrl);
            Console.WriteLine($"TryPop on empty stack returned: {poppedEmpty} (Result: '{emptyUrl}')");
            #endregion

            Console.WriteLine("\nAdvanced C# Assignment 03 completed successfully!");
        }
    }
}
