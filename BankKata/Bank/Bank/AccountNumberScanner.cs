using System;
using System.Collections.Generic;

namespace Bank
{
    public class AccountNumberScanner
    {
        private readonly Dictionary<string, string> numberCodes = new Dictionary<string, string>
        {
            { " _ | ||_|", "0" },
            { "     |  |", "1" },
            { " _  _||_ ", "2" },
            { " _  _| _|", "3" },
            { "   |_|  |", "4" },
            { " _ |_  _|", "5" },
            { " _ |_ |_|", "6" },
            { " _   |  |", "7" },
            { " _ |_||_|", "8" },
            { " _ |_| _|", "9" },
        };

        public string Scan(string account)
        {
            if (account.Length >= 9)
                return numberCodes[account.ExtractFirstNumberCode()] + Scan(account.ExtractPendingNumbersCode());
            return string.Empty;
        }
    }
}
