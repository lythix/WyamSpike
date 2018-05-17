using System;

namespace Wyam
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var lib = new Library();
            var x = lib.GetAnInt();
            var y = lib.Subtract(x, 3);
        }
    }
}
