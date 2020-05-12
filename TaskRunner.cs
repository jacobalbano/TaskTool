using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskTool.Common;
using TaskTool.Logging;
using TaskTool.Logging.Endpoints;
using TaskTool.UI;

namespace TaskTool
{
    public static class TaskRunner
    {
        public static void RunAsSubtask<TTask, TConfig, TSubtask, TSubtaskConfig>(this TaskBase<TTask, TConfig> callingTask, TaskConfigBase<TSubtask, TSubtaskConfig> subtaskConfig)
            where TTask : TaskBase<TTask, TConfig>, new()
            where TConfig : TaskConfigBase<TTask, TConfig>, new()
            where TSubtask : TaskBase<TSubtask, TSubtaskConfig>, new()
            where TSubtaskConfig : TaskConfigBase<TSubtask, TSubtaskConfig>, new()
        {
            Phase.Run("Run subtask", () => DoRun(((ITaskBaseInternal)callingTask).Controller, subtaskConfig));
        }


        public static void Run<TTask, TConfig>(RunParams runParams)
            where TTask : TaskBase<TTask, TConfig>, new()
            where TConfig : TaskConfigBase<TTask, TConfig>, new()
        {
            var config = Phase.Run("Create config", () => CreateConfig<TConfig>(runParams.ConfigFullPath));
            if (runParams.ShowUi)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TaskToolWindow(runParams, typeof(TConfig), config, (cfg, ctrl) => RunInternal(ctrl, (TConfig)cfg)));
            }
            else
            {
                using (var logger = new Logger(new ConsoleEndpoint(), FileEndpoint.CreateForLogFile()))
                    RunInternal(new ControllerImpl(logger), config);
            }
        }

        public static void Run<TTask, TConfig>(string[] args)
            where TTask : TaskBase<TTask, TConfig>, new()
            where TConfig : TaskConfigBase<TTask, TConfig>, new()
        {
            if (args.FirstOrDefault() == "/?")
                Console.WriteLine(RunParams.GetHelp());
            else
                Run<TTask, TConfig>(RunParams.FromConsoleArgs(args));
        }

        private static void RunInternal<TTask, TConfig>(ITaskController controller, TaskConfigBase<TTask, TConfig> config)
            where TTask : TaskBase<TTask, TConfig>, new()
            where TConfig : TaskConfigBase<TTask, TConfig>, new()
        {
            try
            {
                var configStr = Phase.Run("Serialize config", () => JsonConvert.SerializeObject(config, Formatting.Indented));
                config = Phase.Run("Clone config for run", () => JsonConvert.DeserializeObject<TConfig>(configStr));

                var l = controller.Logger;

                using (l.Context("Config:"))
                    controller.Logger.LogLine(configStr);

                DoRun(controller, config);
                controller.Logger.LogLine("-- Done -- ");
            }
            catch (CancelTaskException)
            {
                controller.Logger.LogLine("Task was canceled at the user's request");
            }
            catch (Exception e)
            {
                controller.Logger.LogException(e);
                throw;
            }
        }

        private static void DoRun<TTask, TConfig>(ITaskController controller, TaskConfigBase<TTask, TConfig> taskConfig)
            where TTask : TaskBase<TTask, TConfig>, new()
            where TConfig : TaskConfigBase<TTask, TConfig>, new()
        {
            var task = Phase.Run("Create task", () => new TTask());
            var taskInternal = Phase.Run("Get ITaskInternal", () => (ITaskBaseInternal)task);
            Phase.Run("Set controller", () => taskInternal.Controller = controller);
            Phase.Run("Task run", () => taskInternal.Run((TConfig) taskConfig));
        }

        private static TConfig CreateConfig<TConfig>(string configFullPath)
            where TConfig : new()
        {
            if (configFullPath == null)
                return new TConfig();

            var configText = File.ReadAllText(configFullPath);
            return JsonConvert.DeserializeObject<TConfig>(configText);
        }

        private class ControllerImpl : ITaskController
        {
            public Logger Logger { get; }

            public ControllerImpl(Logger logger)
            {
                Logger = logger;
            }

            //  only the UI implementation has any logic here
            public void AllowCancel() {  }
        }
    }
}
