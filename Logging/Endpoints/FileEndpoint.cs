using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TaskTool.Logging.Endpoints
{
    public sealed class FileEndpoint : ILoggerEndpoint
    {
        public  static FileEndpoint CreateForLogFile()
        {
            var asm = Assembly.GetEntryAssembly();
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var now = DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
            var logsRoot = Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(asm.Location), "logs", today));

            string filename = null;
            var preferredFilename = Path.Combine(logsRoot.FullName, now + "_" + asm.GetName().Name + "_.txt");
            if (!File.Exists(preferredFilename))
                filename = preferredFilename;
            else for (int i = 2; i < 1000; i++)
                {
                    var potentialFilename = Path.Combine(logsRoot.FullName, now + "_" + i.ToString() + "_" + asm.GetName().Name + "_.txt");
                    if (!File.Exists(potentialFilename))
                        filename = potentialFilename;
                }

            if (filename == null)
                throw new Exception("Failed to create a new log file in the specified location");

            return new FileEndpoint(File.Create(filename));
        }

        public FileEndpoint(string fullFilePath) : this(File.Open(fullFilePath, FileMode.Open, FileAccess.Write, FileShare.Read))
        {
        }

        public FileEndpoint(FileStream filestream)
        {
            stream = filestream;
            writer = new StreamWriter(stream);
        }

        public void WriteLine(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public void Dispose()
        {
            writer.Dispose();
            stream.Dispose();
        }

        private readonly FileStream stream;
        private readonly StreamWriter writer;
    }
}
