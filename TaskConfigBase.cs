namespace TaskTool
{
    public class TaskConfigBase<TTask, TConfig>
        where TTask : TaskBase<TTask, TConfig>, new()
        where TConfig : TaskConfigBase<TTask, TConfig>, new()
    {
    }
}