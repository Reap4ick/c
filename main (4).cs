using System;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime ManufactureDate { get; set; }
    public string Country { get; set; }
    public Category Category { get; set; }
}

public class Category
{
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        List<Product> products = GetProducts();

        var currentYearProducts = products
            .Where(p => p.ManufactureDate.Year == DateTime.Now.Year)
            .OrderByDescending(p => p.Price);
        Console.WriteLine("Task 1:");
        Console.WriteLine("Products manufactured in the current year, from most expensive to cheapest:");
        foreach (var product in currentYearProducts)
        {
            Console.WriteLine($"{product.Name} - {product.Price}");
        }
        Console.WriteLine();

        string selectedCountry = "USA";
        int productsInSelectedCountryCount = products.Count(p => p.Country == selectedCountry);
        Console.WriteLine("Task 2:");
        Console.WriteLine($"Number of products manufactured in {selectedCountry}: {productsInSelectedCountryCount}");
        Console.WriteLine();

        string selectedCategoryName = "Electronics";
        var selectedCategoryProducts = products.Where(p => p.Category.Name == selectedCategoryName);
        var mostExpensiveProduct = selectedCategoryProducts.OrderByDescending(p => p.Price).FirstOrDefault();
        var cheapestProduct = selectedCategoryProducts.OrderBy(p => p.Price).FirstOrDefault();
        Console.WriteLine("Task 3:");
        Console.WriteLine($"Most expensive product in the category {selectedCategoryName}: {mostExpensiveProduct?.Name} - {mostExpensiveProduct?.Price}");
        Console.WriteLine($"Cheapest product in the category {selectedCategoryName}: {cheapestProduct?.Name} - {cheapestProduct?.Price}");
        Console.WriteLine();

        var categoriesNotInUkraine = products
            .Where(p => p.Country != "Ukraine")
            .Select(p => p.Category.Name)
            .Distinct();
        Console.WriteLine("Task 4:");
        Console.WriteLine("Names of categories whose products are not manufactured in Ukraine:");
        foreach (var categoryName in categoriesNotInUkraine)
        {
            Console.WriteLine(categoryName);
        }
        Console.WriteLine();

        var productsPerCategory = products
            .GroupBy(p => p.Category.Name)
            .Select(group => new { Category = group.Key, Count = group.Count() });
        Console.WriteLine("Task 5:");
        Console.WriteLine("Number of products in each category:");
        foreach (var categoryCount in productsPerCategory)
        {
            Console.WriteLine($"{categoryCount.Category}: {categoryCount.Count} products");
        }
        Console.WriteLine();
        var groupedProducts = products
            .GroupBy(p => p.Category)
            .Select(group => new
            {
                Category = group.Key,
                Products = group.OrderBy(p => p.ManufactureDate)
            });
        Console.WriteLine("Task 6:");
        Console.WriteLine("Grouping products by categories and sorting by manufacture date:");
        foreach (var group in groupedProducts)
        {
            Console.WriteLine($"Category: {group.Category.Name}");
            foreach (var product in group.Products)
            {
                Console.WriteLine($"    {product.Name} - {product.ManufactureDate}");
            }
        }
    }

    static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product { Name = "Product1", Price = 100, ManufactureDate = new DateTime(2022, 1, 15), Country = "USA", Category = new Category { Name = "Electronics" } },
            new Product { Name = "Product2", Price = 150, ManufactureDate = new DateTime(2023, 5, 20), Country = "China", Category = new Category { Name = "Clothing" } },
            new Product { Name = "Product3", Price = 80, ManufactureDate = new DateTime(2022, 3, 10), Country = "Ukraine", Category = new Category { Name = "Electronics" } },

        };
    }
}
