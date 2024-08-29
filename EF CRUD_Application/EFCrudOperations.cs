using EF_CRUD_Application.Data;
using EF_CRUD_Application.Models;
using System;
using System.Linq;

namespace EF_CRUD_Application
{
    public class EFCrudOperations
    {
        public static void CreateProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter product price: ");
            int price;
            while (!int.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Invalid price. Enter again: ");
            }

            using (var context = new AppDbContext())
            {
                var product = new Product { Name = name, Price = price };
                context.Products.Add(product);
                context.SaveChanges();
                Console.WriteLine("Product inserted successfully.");
            }
        }

        public static void ReadProducts()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var products = context.Products.ToList();
                    Console.WriteLine("\n--- List of Products ---");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        public static void UpdateProduct()
        {
            Console.Write("Enter the ID of the product to update: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid ID. Enter again: ");
            }

            using (var context = new AppDbContext())
            {
                var product = context.Products.Find(id);
                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    return;
                }

                Console.Write($"Enter new product name (current: {product.Name}): ");
                string name = Console.ReadLine();
                product.Name = string.IsNullOrWhiteSpace(name) ? product.Name : name;

                Console.Write($"Enter new product price (current: {product.Price}): ");
                if (int.TryParse(Console.ReadLine(), out int price))
                {
                    product.Price = price;
                }

                context.SaveChanges();
                Console.WriteLine("Product updated successfully.");
            }
        }

        public static void DeleteProduct()
        {
            Console.Write("Enter the ID of the product to delete: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid ID. Enter again: ");
            }

            using (var context = new AppDbContext())
            {
                var product = context.Products.Find(id);
                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    return;
                }

                context.Products.Remove(product);
                context.SaveChanges();
                Console.WriteLine("Product deleted successfully.");
            }
        }
    }
}
