using System;
using System.Data.SqlClient;

namespace EF_CRUD_Application
{
    public class AdoCrudOperations
    {
        private static string connectionString = "Data Source=DESKTOP-HSSFLUB\\SQLEXPRESS;Initial Catalog=db_shispare;Integrated Security=True;Pooling=False;Encrypt=False";

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

            using (SqlConnection conn = new SqlConnection(connectionString))
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
        }

        public static void ReadProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
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
        }

        public static void UpdateProduct()
        {
            Console.Write("Enter the ID of the product to update: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid ID. Enter again: ");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
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
        }

        public static void DeleteProduct()
        {
            Console.Write("Enter the ID of the product to delete: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid ID. Enter again: ");
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
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
        }
    }
}
