using System;
using System.Collections.Generic;
using System.Linq;
using ORMLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ORMLibrary.Repositories
{
    public class OrderRepository
    {
        private readonly ORMOrderProductContext _context;

        public OrderRepository()
        {
            _context = new ORMOrderProductContext();
        }

        public void Create(ORMOrder order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public ORMOrder Read(int id)
        {
            return _context.Orders.Find(id);
        }

        public void Update(ORMOrder order)
        {
            var toUpdate = _context.Orders.Find(order.Id);
            if (toUpdate != null)
            {
                _context.Entry(toUpdate).CurrentValues.SetValues(order);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var toDelete = _context.Orders.Find(id);
            if (toDelete != null)
            {
                _context.Orders.Remove(toDelete);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ORMOrder> Read(int? month = null,
            OrderStatus? status = null,
            int? year = null,
            int? productId = null)
        {
            return _context
                .Orders
                .FromSqlInterpolated($"GetOrders {month}, {status}, {year}, {productId}")
                .ToList();
        }

        public void Delete(int? month = null,
            OrderStatus? status = null,
            int? year = null,
            int? productId = null)
        {
            _context
                .Database
                .ExecuteSqlInterpolated($"DeleteOrders {month}, {status}, {year}, {productId}");
        }
    }
}
