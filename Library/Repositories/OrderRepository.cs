using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Library.Models;

namespace Library.Repositories
{
    public class OrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(Order order)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var query = "Insert into [Order] " +
                        "(Status, CreatedDate, UpdatedDate, ProductId) " +
                        "values (@Status, @CreatedDate , @UpdatedDate, @ProductId); " +
                        "SELECT SCOPE_IDENTITY();";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Status", (int)order.Status);
            command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
            command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
            command.Parameters.AddWithValue("@ProductId", order.ProductId);
            order.Id = Convert.ToInt32(command.ExecuteScalar());
        }

        public Order Read(int id)
        {
            Order order = null;

            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM [Order] WHERE Id = " + id;
            var command = new SqlCommand(query, connection);
            connection.Open();
            var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                order = new Order
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Status = (OrderStatus)dataReader["Status"],
                    CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]),
                    UpdatedDate = Convert.ToDateTime(dataReader["UpdatedDate"]),
                    ProductId = Convert.ToInt32(dataReader["ProductId"])
                };
            }

            return order;
        }

        public void Update(Order order)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "update [Order] " +
                        "set Status = @Status, " +
                        "CreatedDate = @CreatedDate, " +
                        "UpdatedDate = @UpdatedDate, " +
                        "ProductId = @ProductId " +
                        "where Id = @Id";
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", order.Id);
            command.Parameters.AddWithValue("@Status", (int)order.Status);
            command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
            command.Parameters.AddWithValue("@UpdatedDate", order.UpdatedDate);
            command.Parameters.AddWithValue("@ProductId", order.ProductId);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "delete from [Order] where Id = " + id;
            var command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
        }

        public IEnumerable<Order> Read(int? month = null,
            OrderStatus? status = null,
            int? year = null,
            int? productId = null)
        {
            var orders = new List<Order>();
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("GetOrders", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@month", month);
            command.Parameters.AddWithValue("@status", (int?)status);
            command.Parameters.AddWithValue("@year", year);
            command.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            var rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                var order = new Order
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Status = (OrderStatus)rdr["Status"],
                    CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]),
                    UpdatedDate = Convert.ToDateTime(rdr["UpdatedDate"]),
                    ProductId = Convert.ToInt32(rdr["ProductId"])
                };
                orders.Add(order);
            }

            return orders;
        }

        public void Delete(int? month = null,
            OrderStatus? status = null,
            int? year = null,
            int? productId = null)
        {
            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand("DeleteOrders", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
