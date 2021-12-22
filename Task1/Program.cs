using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            foreach(var kek in args)
            {
                try
                {
                    Console.WriteLine(kek[0]);
                    Console.ReadLine();
                }
                catch(NullReferenceException)
                {
                    throw new NullReferenceException();
                }
            }
        }
    }
}