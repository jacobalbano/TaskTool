using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTool.Logging
{
    public interface ILoggerEndpoint : IDisposable
    {
        void WriteLine(string message);
    }
}
