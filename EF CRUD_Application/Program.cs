using EF_CRUD_Application.Data;
using EF_CRUD_Application.Models;
using System;

namespace EF_CRUD_Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- CRUD Console Application ---");
                Console.WriteLine("1. CRUD operations using Entity Framework");
                Console.WriteLine("2. CRUD operations using ADO.NET");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CrudWithEF();
                        break;
                    case "2":
                        CrudWithAdoNet();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CrudWithEF()
        {
            while (true)
            {
                Console.WriteLine("\n--- CRUD Operations with Entity Framework ---");
                Console.WriteLine("1. Create a new product");
                Console.WriteLine("2. Read all products");
                Console.WriteLine("3. Update a product");
                Console.WriteLine("4. Delete a product");
                Console.WriteLine("5. Back to main menu");
                Console.Write("Choose an option: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        EFCrudOperations.CreateProduct();
                        break;
                    case "2":
                        EFCrudOperations.ReadProducts();
                        break;
                    case "3":
                        EFCrudOperations.UpdateProduct();
                        break;
                    case "4":
                        EFCrudOperations.DeleteProduct();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CrudWithAdoNet()
        {
            while (true)
            {
                Console.WriteLine("\n--- CRUD Operations with ADO.NET ---");
                Console.WriteLine("1. Create a new product");
                Console.WriteLine("2. Read all products");
                Console.WriteLine("3. Update a product");
                Console.WriteLine("4. Delete a product");
                Console.WriteLine("5. Back to main menu");
                Console.Write("Choose an option: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AdoCrudOperations.CreateProduct();
                        break;
                    case "2":
                        AdoCrudOperations.ReadProducts();
                        break;
                    case "3":
                        AdoCrudOperations.UpdateProduct();
                        break;
                    case "4":
                        AdoCrudOperations.DeleteProduct();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
