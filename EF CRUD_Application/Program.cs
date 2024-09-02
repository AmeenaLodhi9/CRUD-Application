using System;
using Unity;

namespace EF_CRUD_Application
{
    internal class Program
    {
        private static IUnityContainer _container;

        static void Main(string[] args)
        {
            // Use UnityConfig to configure the container
            _container = UnityConfig.Container;

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
                        Console.ReadLine();
                        Logger.Instance.Log("Exiting the application.", string.Empty);



                        break;
                    }

                    ICrudOperations crudOperations = CrudOperationsFactory.GetCrudOperations(choice, _container);

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

                                Logger.Instance.Log("Back to the main menu", string.Empty);

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
                    Logger.Instance.Log("ArgumentException: " + ex.Message, ex.StackTrace);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An unexpected error occurred. The application will now close.");
                    Logger.Instance.Log(ex.Message, ex.ToString());
                    break; // Exit the loop and terminate the application
                }
            }
        }
    }
}
