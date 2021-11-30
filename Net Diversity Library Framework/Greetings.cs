using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_Diversity_Library_Framework
{
    public static class Greetings
    {
        public static string Hello(string username)
        {
            return string.Concat(DateTime.Now.ToString("h:mm:ss tt"), "Hello ", username, '!');
        }
    }
}
