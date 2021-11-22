using Zork.Common;
using System;

namespace Zork
{
    internal class ConsoleOutputService : IOutputService
    {
        public void Write(object value)
        {
            Console.Write(value);
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }
        public void Write(string value)
        {
            Console.Write(value);
        }
        public void Writeline(string value)
        {
            Console.WriteLine(value);
        }

        public void Clear()
        {
        }
    }
}
