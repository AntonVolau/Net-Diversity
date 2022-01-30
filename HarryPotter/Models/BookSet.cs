using HarryPotter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HarryPotter.Models
{
    internal class BookSet
    {
        private readonly List<Book> books = new List<Book>();
        private readonly DiscountCatalog discounts = new DiscountCatalog();

        public double Price
        {
            get
            {
                var discount = discounts.GetDiscount(books.Count);
                return books.Sum(x => x.Price) * discount;
            }
        }

        public bool Contains(Book book)
        {
            return books.Exists(x => x.Volume == book.Volume);
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            books.Remove(book);
        }

        public string PrintBookSet()
        {
            string output = null;
            foreach (BookEnum bookVolume in Enum.GetValues(typeof(BookEnum)))
            {
                string temp = books.Exists(x => x.Volume == bookVolume) ? "1" : "0";
                output = output == null ? temp : $"{output}, {temp}";
            }
            return output;
        }
    }
}
