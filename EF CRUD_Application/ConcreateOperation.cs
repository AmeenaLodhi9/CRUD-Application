using EF_CRUD_Application.Data;
using EF_CRUD_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace EF_CRUD_Application
{
    public class EFCrudOperations : ICrudOperations
    {
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
                Logger.Log("Product created successfully", string.Empty); // Log success message
            }
            catch (Exception ex)
            {
                Logger.Log("Error creating product", ex.ToString()); // Log error message
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
                Logger.Log("Product Read successfully", string.Empty); // Log success message

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

                Logger.Log("Product Updated successfully", string.Empty); // Log success message
            }
            catch (Exception ex)
            {
                Logger.Log("Error creating product", ex.ToString()); // Log error message
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

                Logger.Log("Product deleted successfully", string.Empty); // Log success message
            }
            catch (Exception ex)
            {
                Logger.Log("Error creating product", ex.ToString()); // Log error message
                throw; // Optionally re-throw the exception
            }
            
        }
        public class AdoCrudOperations : ICrudOperations
        {
            private readonly string _connectionString = "Data Source=DESKTOP-HSSFLUB\\SQLEXPRESS;Initial Catalog=db_shispare;Integrated Security=True;Pooling=False;Encrypt=False";
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

                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        string query = "INSERT INTO tbl_products (prod_name, prod_price) VALUES (@name, @price)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@price", price);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Product inserted successfully.");
                        }
                    }

                    Logger.Log("Product created successfully", string.Empty); // Log success message
                }
                catch (Exception ex)
                {
                    Logger.Log("Error creating product", ex.ToString()); // Log error message
                    throw; // Optionally re-throw the exception
                }

                
            }

            public void ReadProducts()
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        string query = "SELECT * FROM tbl_products";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                Console.WriteLine("\n--- List of Products ---");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"ID: {reader["prod_id"]}, Name: {reader["prod_name"]}, Price: {reader["prod_price"]}");
                                }
                            }
                        }
                    }

                    Logger.Log("Product read successfully", string.Empty); // Log success message
                }
                catch (Exception ex)
                {
                    Logger.Log("Error creating product", ex.ToString()); // Log error message
                    throw; // Optionally re-throw the exception
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

                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        string query = "SELECT prod_name, prod_price FROM tbl_products WHERE prod_id = @id";
                        string currentName = null;
                        int currentPrice = 0;

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    currentName = reader["prod_name"].ToString();
                                    currentPrice = Convert.ToInt32(reader["prod_price"]);
                                }
                                else
                                {
                                    Console.WriteLine("Product not found.");
                                    return;
                                }
                            }
                        }

                        Console.Write($"Enter new product name (current: {currentName}): ");
                        string name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            name = currentName;
                        }

                        Console.Write($"Enter new product price (current: {currentPrice}): ");
                        if (!int.TryParse(Console.ReadLine(), out int price))
                        {
                            price = currentPrice;
                        }

                        string updateQuery = "UPDATE tbl_products SET prod_name = @name, prod_price = @price WHERE prod_id = @id";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@price", price);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Product updated successfully.");
                        }
                    }

                    Logger.Log("Product updated successfully", string.Empty); // Log success message
                }
                catch (Exception ex)
                {
                    Logger.Log("Error creating product", ex.ToString()); // Log error message
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

                    using (SqlConnection conn = new SqlConnection(_connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM tbl_products WHERE prod_id = @id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Product deleted successfully.");
                            }
                            else
                            {
                                Console.WriteLine("Product not found.");
                            }
                        }
                    }

                    Logger.Log("Product deleted successfully", string.Empty); // Log success message
                }
                catch (Exception ex)
                {
                    Logger.Log("Error creating product", ex.ToString()); // Log error message
                    throw; // Optionally re-throw the exception
                }
                
            }

        }    
    }
}
