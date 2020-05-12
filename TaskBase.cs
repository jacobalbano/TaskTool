using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTool.Common;

namespace TaskTool
{
    public abstract class TaskBase<TTask, TConfig> : ITaskBaseInternal
        where TTask : TaskBase<TTask, TConfig>, new()
        where TConfig : TaskConfigBase<TTask, TConfig>, new()
    {
        public Logger Logger => Controller.Logger;

        ITaskController ITaskBaseInternal.Controller
        {
            get => Controller;
            set
            {
                if (Controller != null)
                    throw new Exception("Failed to set task controller; was task object reused across runs?");
                Controller = value;
            }
        }

        protected void AllowCancel() => Controller.AllowCancel();

        protected abstract void DoWork(TConfig config);

        void ITaskBaseInternal.Run(object config)
        {
            DoWork((TConfig)config);
        }

        private ITaskController Controller;
    }
}
