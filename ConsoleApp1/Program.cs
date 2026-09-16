using System;
using System.Collections.Generic;

namespace AdvancedCSharp02
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } // "Electronics", "Clothing", "Food", "Books"
        public double Price { get; set; }
        public int Stock { get; set; }
    }

    class Program
    {
        // Task 01: Smart Product Search using Func<Product, bool>
        // Func<Product, bool> is used here because filtering requires taking a Product as input
        // and returning a boolean (true if matches, false otherwise).
        public static List<Product> SearchProducts(List<Product> list, Func<Product, bool> filter)
        {
            List<Product> results = new List<Product>();
            foreach (var p in list)
            {
                if (filter(p))
                    results.Add(p);
            }
            return results;
        }

        // Task 03.1: Print Reports using Action<Product>
        // Action<Product> is used here because printing performs an action without returning any value.
        public static void PrintReport(List<Product> list, Action<Product> reportAction)
        {
            foreach (var p in list)
            {
                reportAction(p);
            }
        }

        // Task 03.2: Transform Products using Func<Product, TResult>
        // Func<Product, TResult> is used here to map/transform each Product into a new result value.
        public static List<TResult> TransformProducts<TResult>(List<Product> list, Func<Product, TResult> transform)
        {
            List<TResult> results = new List<TResult>();
            foreach (var p in list)
            {
                results.Add(transform(p));
            }
            return results;
        }

        // Task 03.3: Filter Products using Predicate<Product>
        // Predicate<Product> is used here as a specialized delegate for testing boolean conditions.
        public static List<Product> FilterProducts(List<Product> list, Predicate<Product> predicate)
        {
            List<Product> results = new List<Product>();
            foreach (var p in list)
            {
                if (predicate(p))
                    results.Add(p);
            }
            return results;
        }

        static void Main(string[] args)
        {
            List<Product> catalog = new()
            {
                new Product { Id=1, Name="Laptop", Category="Electronics", Price=1200, Stock=10 },
                new Product { Id=2, Name="Phone", Category="Electronics", Price=800, Stock=25 },
                new Product { Id=3, Name="T-Shirt", Category="Clothing", Price=30, Stock=100 },
                new Product { Id=4, Name="Jeans", Category="Clothing", Price=60, Stock=50 },
                new Product { Id=5, Name="Chocolate", Category="Food", Price=5, Stock=200 },
                new Product { Id=6, Name="Coffee Beans", Category="Food", Price=15, Stock=80 },
                new Product { Id=7, Name="C# Book", Category="Books", Price=45, Stock=30 },
                new Product { Id=8, Name="Novel", Category="Books", Price=20, Stock=60 },
                new Product { Id=9, Name="Headphones", Category="Electronics", Price=150, Stock=40 },
                new Product { Id=10, Name="Jacket", Category="Clothing", Price=120, Stock=15 }
            };

            #region Task 01: Smart Product Search
            Console.WriteLine("--- Electronics ---");
            var electronics = SearchProducts(catalog, p => p.Category == "Electronics");
            electronics.ForEach(p => Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})"));

            Console.WriteLine("\n--- Under $50 ---");
            var under50 = SearchProducts(catalog, p => p.Price < 50);
            under50.ForEach(p => Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})"));

            Console.WriteLine("\n--- In Stock ---");
            var inStock = SearchProducts(catalog, p => p.Stock > 0);
            inStock.ForEach(p => Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})"));

            Console.WriteLine("\n--- Clothing Under $100 ---");
            var clothingUnder100 = SearchProducts(catalog, p => p.Category == "Clothing" && p.Price < 100);
            clothingUnder100.ForEach(p => Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})"));
            #endregion

            #region Task 03.1: Print Reports (Action)
            Console.WriteLine("\n--- Short Report ---");
            PrintReport(catalog, p => Console.WriteLine($"{p.Name} - ${p.Price}"));

            Console.WriteLine("\n--- Detailed Report ---");
            PrintReport(catalog, p => Console.WriteLine($"[{p.Category}] {p.Name} | Price: ${p.Price} | Stock: {p.Stock}"));
            #endregion

            #region Task 03.2: Transform Products (Func)
            Console.WriteLine("\n--- Summary List ---");
            var summaries = TransformProducts(catalog, p => $"{p.Name} (${p.Price})");
            summaries.ForEach(s => Console.WriteLine(s));

            Console.WriteLine("\n--- Price Labels ---");
            var priceLabels = TransformProducts(catalog, p => $"{p.Name}: {(p.Price > 100 ? "Expensive!" : "Affordable")}");
            priceLabels.ForEach(lbl => Console.WriteLine(lbl));
            #endregion

            #region Task 03.3: Filter Products (Predicate)
            Console.WriteLine("\n--- Low-Stock Alert ---");
            var lowStock = FilterProducts(catalog, p => p.Stock < 20);
            lowStock.ForEach(p => Console.WriteLine($"[LOW STOCK] {p.Name}: only {p.Stock} left!"));
            #endregion

            Console.WriteLine("\nAdvanced C# Assignment 02 completed successfully!");
        }
    }
}
