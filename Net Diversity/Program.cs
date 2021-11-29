using Net_Diversity_Library;
using System;

namespace Net_Diversity
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Please, type your name for task 1:");
            Console.WriteLine(string.Concat("Hello ", Console.ReadLine(), '!'));
            Console.WriteLine("Please, type your name for task 2:");
            Console.WriteLine(Greetings.Hello(Console.ReadLine()));
        }
    }
}
