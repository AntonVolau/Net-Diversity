using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public static class StringExtensions
    {
        public static string ExtractFirstNumberCode(this string input)
        {
            var inputLines = input.Split('\n');
            return inputLines[0].Substring(0, 3) + inputLines[1].Substring(0, 3) + inputLines[2].Substring(0, 3);
        }

        public static string ExtractPendingNumbersCode(this string input)
        {
            var inputLines = input.Split('\n');
            var pendingFirstLine = inputLines[0].Substring(3, inputLines[0].Length - 3);
            var pendingSecondLine = inputLines[1].Substring(3, inputLines[1].Length - 3);
            var pendingThirdLine = inputLines[2].Substring(3, inputLines[2].Length - 3);
            return pendingFirstLine + "\n" + pendingSecondLine + "\n" + pendingThirdLine;
        }
    }
}
