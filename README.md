# TaskTool
Framework for writing quick tools with simple UI

Tools can be run directly or from the command line with various options to customize behavior (run with the /? switch to see all options).

# Example program
```csharp
using TaskTool;

class Program
{
    [STAThread] //  use STAThread so open/close file dialogs will work
    static void Main(string[] args)
    {
        TaskRunner.Run<TestTask, TestTask.Config>(args);
    }
}

class TestTask : TaskBase<TestTask, TestTask.Config>
{
    public class Config : TaskConfigBase<TestTask, Config>
    {
        public int CountDownFrom { get; set; }

        public MessageConfig SubSettings { get; set; } = new MessageConfig();
    }

    //  use TaskConfigConverter on sub-objects
    //  ExpandableObjectConverter doesn't play nicely with Newtonsoft
    [TypeConverter(typeof(TaskConfigConverter))]
    public class MessageConfig
    {
        public string Message { get; set; }
    }

    protected override void DoWork(Config config)
    {
        for (int i = config.CountDownFrom; i >= 0; i--)
        {
            Logger.LogLine($"{i}...");
            Thread.Sleep(1000);

            //  Call at convenient intervals to allow clean cancellation from the UI
            AllowCancel();
        }

        Logger.LogLine(config.SubSettings.Message);
    }
}
```