using EF_CRUD_Application.Data;
using EF_CRUD_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CRUD_Application
{
    public class EFCrudOperations : ICrudOperations
    {
   
        //Service Class
        public void CreateProduct()
        {
            try
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
                Logger.Instance.Log("Product created successfully", string.Empty); // Log success message
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("Error creating product", ex.ToString()); // Log error message
                throw; // Optionally re-throw the exception
            }

        }

        public void ReadProducts()
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
                Logger.Instance.Log("Product Read successfully", string.Empty); // Log success message

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        public void UpdateProduct()
        {
            try
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

                Logger.Instance.Log("Product Updated successfully", string.Empty); // Log success message
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("Error creating product", ex.ToString()); // Log error message
                throw; // Optionally re-throw the exception
            }




        }

        public void DeleteProduct()
        {
            try
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

                Logger.Instance.Log("Product deleted successfully", string.Empty); // Log success message
            }
            catch (Exception ex)
            {
                Logger.Instance.Log("Error creating product", ex.ToString()); // Log error message
                throw; // Optionally re-throw the exception
            }

        }
    }
}