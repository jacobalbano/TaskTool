using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTool.Common;

namespace TaskTool
{
    internal interface ITaskController
    {
        void AllowCancel();

        Logger Logger { get; }
    }
}
