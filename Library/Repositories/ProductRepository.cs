using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Library.Models;

namespace Library.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = "Insert into Product " +
                        "(Name, Description, Weight, Height, Width, Length) " +
                        "values (@Name, @Description , @Weight, @Height, @Width, @Length); " +
                        "SELECT SCOPE_IDENTITY();";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Weight", product.Weight);
            command.Parameters.AddWithValue("@Height", product.Height);
            command.Parameters.AddWithValue("@Width", product.Width);
            command.Parameters.AddWithValue("@Length", product.Length);
            product.Id = Convert.ToInt32(command.ExecuteScalar());
        }

        public Product Read(int id)
        {
            Product product = null;

            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM Product WHERE Id = " + id;
            var command = new SqlCommand(query, connection);
            connection.Open();
            var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                product = new Product
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Name = dataReader["Name"].ToString(),
                    Description = dataReader["Description"].ToString(),
                    Weight = Convert.ToInt32(dataReader["Weight"]),
                    Height = Convert.ToInt32(dataReader["Height"]),
                    Width = Convert.ToInt32(dataReader["Width"]),
                    Length = Convert.ToInt32(dataReader["Length"])
                };
            }

            return product;
        }

        public void Update(Product product)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "update Product " +
                        "set Name = @Name, " +
                        "Description = @Description, " +
                        "Weight = @Weight, " +
                        "Height = @Height, " +
                        "Width = @Width, " +
                        "Length = @Length " +
                        "where Id = @Id";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", product.Id);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Description", product.Description);
            command.Parameters.AddWithValue("@Weight", product.Weight);
            command.Parameters.AddWithValue("@Height", product.Height);
            command.Parameters.AddWithValue("@Width", product.Width);
            command.Parameters.AddWithValue("@Length", product.Length);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "delete from Product where Id = " + id;
            var command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public IEnumerable<Product> Read()
        {
            var products = new List<Product>();
            using var connection = new SqlConnection(_connectionString);
            var query = "select * from Product";
            var command = new SqlCommand(query, connection);
            connection.Open();
            var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                var product = new Product
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Name = dataReader["Name"].ToString(),
                    Description = dataReader["Description"].ToString(),
                    Weight = Convert.ToInt32(dataReader["Weight"]),
                    Height = Convert.ToInt32(dataReader["Height"]),
                    Width = Convert.ToInt32(dataReader["Width"]),
                    Length = Convert.ToInt32(dataReader["Length"])
                };
                products.Add(product);
            }

            return products;
        }

        public void Delete()
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "delete from Product";
            var command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
