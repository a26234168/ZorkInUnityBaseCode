using System;
using System.Collections.Generic;
using System.Text;

namespace Zork.Common
{
    public interface IOutputService
    {
        void Write(object value);

        void WriteLine(object value);
    }
}
