using System;

namespace App.Output
{
    class ConsolePrinter : IPrinter
    {
        public void PrintResult(string data)
        {
            Console.WriteLine(data);
        }
    }
}
