using TaskTool.Common;

namespace TaskTool
{
    internal interface ITaskBaseInternal
    {
        ITaskController Controller { get; set; }
        void Run(object config);
    }
}