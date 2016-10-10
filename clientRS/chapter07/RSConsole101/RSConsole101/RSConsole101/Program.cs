using System;
using System.Collections.Generic;
using System.Text;

namespace RSConsole101
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Console Application 101";
            Console.Write("Please enter some text followed by Enter: ");
            String strInput = Console.ReadLine();
            Console.WriteLine("You entered: " + strInput);
            Console.WriteLine("Press any key to exit!");
            Console.Read();
        }
    }
}
