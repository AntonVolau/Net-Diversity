using System;

namespace Net_Diversity_Library
{
    public static class Greetings
    {
        public static string Hello(string username)
        {
            return string.Concat(DateTime.Now.ToString("h:mm:ss tt"), "Hello ", username, '!');
        }
    }
}
