using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTool.Logging;

namespace TaskTool.Common
{
    public sealed class Logger : IDisposable
    {
        public event Action<string> OpenContext;
        public event Action CloseContext;

        public Logger(params ILoggerEndpoint[] endpoints)
        {
            Endpoints = endpoints?.ToList() ?? throw new ArgumentNullException(nameof(endpoints));
            ctx = new ContextImpl(this);
        }

        public IDisposable Context<T>(T message)
        {
            return Context(message?.ToString() ?? "");
        }

        public IDisposable Context(string section)
        {
            LogLine(section);
            depth++;
            OpenContext?.Invoke(section);
            return ctx;
        }

        public void LogLine<T>(T message)
        {
            LogLine(message?.ToString() ?? "");
        }

        public void LogLine(string message)
        {
            if (depth > 0) message = new string('\t', depth) + message;
            foreach (var ep in Endpoints)
                ep.WriteLine(message);
        }

        public void LogException(Exception e)
        {
            using (Context("Error:"))
            {
                var lines = e.ToString().Split("\r\n".ToCharArray());
                foreach (var line in lines)
                    LogLine(line);
            }
        }

        private class ContextImpl : IDisposable
        {
            private readonly Logger logger;

            public ContextImpl(Logger logger)
            {
                this.logger = logger;
            }

            void IDisposable.Dispose()
            {
                if (--logger.depth < 0)
                    throw new Exception("Logger context corruption");

                logger.CloseContext?.Invoke();
            }
        }

        private readonly ContextImpl ctx;
        private readonly List<ILoggerEndpoint> Endpoints;
        private int depth = 0;

        #region IDisposable Support
        private bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    foreach (var endpoint in Endpoints)
                        endpoint.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
