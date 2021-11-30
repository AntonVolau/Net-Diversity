using System;

namespace MultyLib
{
    public static class Greetings
    {
        public static string Hello(string username)
        {
            return string.Concat(DateTime.Now.ToString("h:mm:ss tt"), "Hello ", username, '!');
        }
    }
}
