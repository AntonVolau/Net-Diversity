using Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BankTests
{
    [TestClass]
    public class BankUnitTests
    {
        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_00()
        {
            string expected = "00";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_00(string version)
        {
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{version}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, version);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_000000000()
        {
            string expected = "000000000";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_111111111()
        {
            string expected = "111111111";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_123456789()
        {
            string expected = "123456789";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_222222222()
        {
            string expected = "222222222";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_333333333()
        {
            string expected = "333333333";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }
        
        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_444444444()
        {
            string expected = "444444444";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_555555555()
        {
            string expected = "555555555";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_666666666()
        {
            string expected = "666666666";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_777777777()
        {
            string expected = "777777777";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_888888888()
        {
            string expected = "888888888";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ShouldReturnAccountNumberForTheInput_999999999()
        {
            string expected = "999999999";
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(actual, expected);
        }
    }
}
