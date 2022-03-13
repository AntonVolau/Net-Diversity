using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Library.Models;
using Library.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class OrderRepositoryTests
    {
        private readonly OrderRepository _orderRepository;
        private const string ConnectionString =
            @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=ADO.NET_DB;Integrated Security=True";

        public OrderRepositoryTests()
        {
            _orderRepository = new OrderRepository(ConnectionString);
            _orderRepository.Delete();
        }

        [TestMethod]
        public void Create_Order()
        {
            var order = InsertInitialOrder();
            var result = _orderRepository.Read(order.Id);

            Assert.AreEqual(ToJson(order), ToJson(result));
        }

        [TestMethod]
        public void Read_Orders()
        {
            var order = InsertInitialOrder();
            var orders = new List<Order>
            {
                order
            };

            var result = _orderRepository.Read();

            Assert.AreEqual(ToJson(orders), ToJson(result));
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 0)]

        public void Read_ByProductId_ReturnsOrdersCount(int productId, int count)
        {
            InsertInitialOrders();

            var result = _orderRepository.Read(productId: productId);

            Assert.AreEqual(count, result.Count());
        }

        [TestMethod]
        [DataRow(OrderStatus.InProgress, 1)]
        [DataRow(OrderStatus.Done, 0)]

        public void Read_ByStatus_ReturnsOrdersCount(OrderStatus status, int count)
        {
            InsertInitialOrders();

            var result = _orderRepository.Read(status: status);

            Assert.AreEqual(count, result.Count());
        }

        [TestMethod]
        [DataRow(2022, 1)]
        [DataRow(2021, 0)]

        public void Read_ByYear_ReturnsOrdersCount(int year, int count)
        {
            InsertInitialOrders();

            var result = _orderRepository.Read(year: year);

            Assert.AreEqual(count, result.Count());
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 0)]

        public void Read_ByMonth_ReturnsOrdersCount(int month, int count)
        {
            InsertInitialOrders();

            var result = _orderRepository.Read(month: month);

            Assert.AreEqual(count, result.Count());
        }

        [TestMethod]
        public void Update_Order()
        {
            var order = InsertInitialOrder();

            order.Status = OrderStatus.Done;
            _orderRepository.Update(order);
            var result = _orderRepository.Read(order.Id);

            Assert.AreEqual(ToJson(order), ToJson(result));
        }

        [TestMethod]
        public void Delete_Order()
        {
            var order = InsertInitialOrder();

            _orderRepository.Delete(order.Id);
            var result = _orderRepository.Read(order.Id);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Delete_Orders_ReturnsEmptyList()
        {
            InsertInitialOrders();

            _orderRepository.Delete();
            var result = _orderRepository.Read();

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(4)]

        public void Delete_ByProductId_ReturnsEmptyList(int productId)
        {
            InsertInitialOrders();

            _orderRepository.Delete(productId: productId);
            var result = _orderRepository.Read(productId: productId);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [DataRow(OrderStatus.InProgress)]
        [DataRow(OrderStatus.Arrived)]
        [DataRow(OrderStatus.NotStarted)]

        public void Delete_ByStatus_ReturnsEmptyList(OrderStatus status)
        {
            InsertInitialOrders();

            _orderRepository.Delete(status: status);
            var result = _orderRepository.Read(status: status);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [DataRow(2022)]
        [DataRow(2020)]
        [DataRow(2019)]

        public void Delete_ByYear_ReturnsEmptyList(int year)
        {
            InsertInitialOrders();

            _orderRepository.Delete(year: year);
            var result = _orderRepository.Read(year: year);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(12)]
        [DataRow(11)]

        public void Delete_ByMonth_ReturnsEmptyList(int month)
        {
            InsertInitialOrders();

            _orderRepository.Delete(month: month);
            var result = _orderRepository.Read(month: month);

            Assert.AreEqual(0, result.Count());
        }

        private void InsertInitialOrders()
        {
            var order = new Order
            {
                Status = OrderStatus.InProgress,
                CreatedDate = new DateTime(2022, 1, 6),
                UpdatedDate = new DateTime(2022, 1, 7),
                ProductId = 1
            };
            var order1 = new Order
            {
                Status = OrderStatus.Arrived,
                CreatedDate = new DateTime(2020, 12, 3),
                UpdatedDate = new DateTime(2020, 12, 3),
                ProductId = 3
            };
            var order2 = new Order
            {
                Status = OrderStatus.NotStarted,
                CreatedDate = new DateTime(2019, 11, 2),
                UpdatedDate = new DateTime(2019, 11, 2),
                ProductId = 4
            };

            _orderRepository.Create(order);
            _orderRepository.Create(order1);
            _orderRepository.Create(order2);
        }

        private Order InsertInitialOrder()
        {
            var order = new Order
            {
                Status = OrderStatus.InProgress,
                CreatedDate = new DateTime(2022, 3, 13),
                UpdatedDate = new DateTime(2022, 3, 13),
                ProductId = 1
            };

            _orderRepository.Create(order);

            return order;
        }

        private string ToJson(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
