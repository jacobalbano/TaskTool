using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskTool.Common;
using TaskTool.Logging;
using TaskTool.Logging.Endpoints;

namespace TaskTool.UI
{
    public partial class TaskRunWindow : Form
    {
        public TaskRunWindow()
        {
            InitializeComponent();
            controller = new ControllerImpl(this);
        }

        internal ITaskController TaskController => controller;
        private readonly ControllerImpl controller;

        private sealed class ControllerImpl : ITaskController, ILoggerEndpoint
        {
            public Logger Logger { get; }
            public bool CancellationPending { get; set; }

            public ControllerImpl(TaskRunWindow window)
            {
                Window = window;
                Logger = new Logger(FileEndpoint.CreateForLogFile(), this);
                Logger.OpenContext += PushContext;
                Logger.CloseContext += PopContext;
            }

            public void AllowCancel()
            {
                if (CancellationPending)
                    throw new CancelTaskException();
            }

            void IDisposable.Dispose() { }

            public void PushContext(string section)
            {
                Window.contexts.Push(section);
                Window.UpdateWindow();
            }

            public void PopContext()
            {
                Window.contexts.TryPop(out _);
                Window.UpdateWindow();
            }

            void ILoggerEndpoint.WriteLine(string message)
            {
                Window.messages.Enqueue(Window.lastMessage = message);
                Window.UpdateWindow();
            }

            private readonly TaskRunWindow Window;
        }

        internal void StartWork(Action work)
        {
            workerThread = new Thread(() =>
            {
                working = true;

                using (controller.Logger)
                    work();

                working = false;
                Invoke(new Action(() => btnAbort.Enabled = btnCancel.Enabled = false));
            });
            workerThread.Start();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            controller.CancellationPending = true;
            btnCancel.Enabled = false;
        }

        private void abort_Click(object sender, EventArgs e)
        {
            workerThread.Abort();
            controller.Logger.LogLine("Task was aborted at user's request");
            btnAbort.Enabled = false;
            btnCancel.Enabled = false;
            working = false;
        }

        private void TaskRunWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (working)
            {
                MessageBox.Show("A task is still running. Abort or cancel it, or wait for it to complete before closing this window.", "Task running", MessageBoxButtons.OK);
                e.Cancel = true;
            }
        }

        private void cbUpdateOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUpdateOutput.Checked)
                UpdateWindow();
        }

        private void UpdateWindow()
        {
            if (!IsHandleCreated)
                return;

            var summmarySb = new StringBuilder();
            foreach (var str in contexts.Reverse())
                summmarySb.AppendLine(str);

            var lastM = lastMessage;
            if (lastM != null)
                summmarySb.AppendLine(lastMessage.Trim());

            var messageSb = new StringBuilder();
            while (cbUpdateOutput.Checked && messages.TryDequeue(out var s))
                messageSb.AppendLine(s);

            Invoke(new Action(() =>
            {
                var output = tbLiveOutput;
                var summary = tbSummary;

                if (messageSb.Length > 0)
                    output.AppendText(messageSb.ToString());

                summary.Text = "";
                summary.AppendText(summmarySb.ToString());
            }));
        }

        private Thread workerThread;
        private volatile bool working;

        private volatile string lastMessage;
        private readonly ConcurrentQueue<string> messages = new ConcurrentQueue<string>();
        private readonly ConcurrentStack<string> contexts = new ConcurrentStack<string>();
    }
}
