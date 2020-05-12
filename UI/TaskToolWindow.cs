using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskTool.Logging;

namespace TaskTool.UI
{
    public partial class TaskToolWindow : Form
    {
        internal TaskToolWindow(RunParams runParams, Type configType, object config,  Action<object, ITaskController> runTask)
        {
            InitializeComponent();
            pgConfigObject.SelectedObject = config;
            ConfigType = configType;

            RunTask = runTask;
            if (!runParams.Wait)
                DoRunTask();
        }

        private void DoRunTask()
        {
            var loggerWindow = new TaskRunWindow();
            loggerWindow.StartWork(() => RunTask(pgConfigObject.SelectedObject, loggerWindow.TaskController));
            loggerWindow.Show(this);
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            DoRunTask();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Discard current configuration?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedConfigPath = openFileDialog1.FileName;
                var cfg = File.ReadAllText(selectedConfigPath);
                var configObj = JsonConvert.DeserializeObject(cfg, ConfigType);
                pgConfigObject.SelectedObject = configObj;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedConfigPath == null)
                saveAsToolStripMenuItem_Click(sender, e);
            else
                File.WriteAllText(selectedConfigPath, JsonConvert.SerializeObject(pgConfigObject.SelectedObject, Formatting.Indented));
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedConfigPath = saveFileDialog1.FileName;
                File.WriteAllText(selectedConfigPath, JsonConvert.SerializeObject(pgConfigObject.SelectedObject, Formatting.Indented));
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string selectedConfigPath;
        private Action<object, ITaskController> RunTask { get; }
        private Type ConfigType { get; }
    }
}
