using Bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BankTests
{
    [TestClass]
    public class BankUnitTests
    {
        [TestMethod]
        [DataRow("00")]
        [DataRow("000000000")]
        [DataRow("111111111")]
        [DataRow("222222222")]
        [DataRow("333333333")]
        [DataRow("444444444")]
        [DataRow("555555555")]
        [DataRow("666666666")]
        [DataRow("777777777")]
        [DataRow("888888888")]
        [DataRow("999999999")]
        [DataRow("123456789")]
        public void Calculate_Imput_RetirnAccountNumber(string expected)
        {
            var scanner = new AccountNumberScanner();
            var input = File.ReadAllText($"./TestInputs/{expected}.txt");
            var actual = scanner.Scan(input);
            Assert.AreEqual(actual, expected);
        }
    }
}
