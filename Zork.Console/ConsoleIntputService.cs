using Zork.Common;
using System;

namespace Zork
{
    internal class ConsoleIntputService :  IInputService
    {
        public event EventHandler<string> InputReceived;


        public void ProcessInput()
        {
            string inputString = Console.ReadLine().Trim().ToUpper();
            InputReceived?.Invoke(this, inputString);
        }
    }

}

    

