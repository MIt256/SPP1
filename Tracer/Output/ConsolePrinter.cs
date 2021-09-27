using System;
using System.Collections.Generic;
using System.Text;

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