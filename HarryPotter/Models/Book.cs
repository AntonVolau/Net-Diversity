using HarryPotter.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HarryPotter.Models
{
    public class Book
    {
        public double Price { get; } = 8;
        public BookEnum Volume { get; }

        public Book(BookEnum volume)
        {
            Volume = volume;
        }
    }
}
