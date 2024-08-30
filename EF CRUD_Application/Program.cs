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
                try
                {
                    Console.WriteLine("\n--- CRUD Console Application ---");
                    Console.WriteLine("1. CRUD operations using Entity Framework");
                    Console.WriteLine("2. CRUD operations using ADO.NET");
                    Console.WriteLine("3. Exit");
                    Console.Write("Choose an option: ");

                    var choice = Console.ReadLine();

                    if (choice == "3")
                    {
                        Console.WriteLine("Exiting the application.");
                        break;
                    }

                    // Log the user's choice
                    Logger.Log("User chose option: " + (choice == "1" ? "Entity Framework" : choice == "2" ? "ADO.NET" : "Invalid Choice"), "");

                    ICrudOperations crudOperations = CrudOperationsFactory.GetCrudOperations(choice);

                    while (true)
                    {
                        Console.WriteLine("\n--- CRUD Operations ---");
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
                                crudOperations.CreateProduct();
                                break;
                            case "2":
                                crudOperations.ReadProducts();
                                break;
                            case "3":
                                crudOperations.UpdateProduct();
                                break;
                            case "4":
                                crudOperations.DeleteProduct();
                                break;
                            case "5":
                                break;
                            default:
                                Console.WriteLine("Invalid option. Please try again.");
                                break;
                        }

                        if (input == "5") break;
                    }

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    // Log and allow retry
                    Logger.Log("ArgumentException: " + ex.Message, ex.StackTrace);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred. The application will now close.");
                    Logger.Log(ex.Message, ex.ToString());
                    break; // Exit the loop and terminate the application
                }
            }
        }
    }
}
