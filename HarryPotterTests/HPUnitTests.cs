using HarryPotter.Enums;
using HarryPotter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HarryPotterTests
{
    [TestClass]
    public class HPUnitTests
    {
        [TestMethod]
        [DataRow(8, new int[5] { 1, 0, 0, 0, 0 })]
        [DataRow(2 * 8 * 0.95, new int[5] { 1, 1, 0, 0, 0 })]
        [DataRow(3 * 8 * 0.9, new int[5] { 1, 1, 1, 0, 0 })]
        [DataRow(5 * 8 * 0.75, new int[5] { 1, 1, 1, 1, 1 })]
        [DataRow(2 * 8 * 0.95 + 1 * 8, new int[5] { 2, 1, 0, 0, 0 })]
        [DataRow(3 * 8 * 0.9 + 1 * 8, new int[5] { 2, 1, 1, 0, 0 })]
        [DataRow(4 * 8 * 0.8 + 4 * 8 * 0.8, new int[5] { 2, 2, 2, 1, 1 })]
        [DataRow(3 * (8 * 5 * 0.75) + 2 * (8 * 4 * 0.8), new int[5] { 5, 5, 4, 5, 4 })]
        public void Calculate_Price_ReturnsValidInt(double expected, int[] bookVolumeQuantities)
        {
            Basket basket = new Basket();
            for (int i = 0; i < bookVolumeQuantities.Length; i++)
            {
                basket.AddBooks((BookEnum)(i + 1), bookVolumeQuantities[i]);
            }

            double actual = basket.TotalPrice;

            Assert.AreEqual(expected, actual);
        }
    }
}
