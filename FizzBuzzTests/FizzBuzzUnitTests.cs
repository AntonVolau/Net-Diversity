using System;
using FizzBuzz;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FizzBuzzUnitTests
{
    [TestClass]
    public class FizzBuzzUnitTests
    {
        [TestMethod]
        [DataRow(6, "Fizz")]
        [DataRow(10, "Buzz")]
        [DataRow(30, "FizzBuzz")]
        [DataRow(26, "26")]
        public void Calculate_Number_ReturnsValidString(int number, string expected)
        {
            var fizzBuzz = new ClassicFizzBuzz();

            var result = fizzBuzz.Print(number);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(122)]
        public void Calculate_Number_ThrowsArgumentOutOfRangeException(int number)
        {
            var fizzBuzz = new ClassicFizzBuzz();
            Func<int, string> func = fizzBuzz.Print;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                func(number));
        }
    }
}
