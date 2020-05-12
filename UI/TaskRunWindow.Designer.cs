namespace TaskTool.UI
{
    partial class TaskRunWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbSummary = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbLiveOutput = new System.Windows.Forms.TextBox();
            this.cbUpdateOutput = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(713, 415);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.Location = new System.Drawing.Point(632, 415);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 2;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.abort_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 396);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(769, 377);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbSummary);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(761, 351);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Summary";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbSummary
            // 
            this.tbSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSummary.Location = new System.Drawing.Point(3, 3);
            this.tbSummary.Multiline = true;
            this.tbSummary.Name = "tbSummary";
            this.tbSummary.ReadOnly = true;
            this.tbSummary.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbSummary.Size = new System.Drawing.Size(755, 345);
            this.tbSummary.TabIndex = 1;
            this.tbSummary.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbLiveOutput);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(761, 351);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Live Output";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbLiveOutput
            // 
            this.tbLiveOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLiveOutput.Location = new System.Drawing.Point(3, 3);
            this.tbLiveOutput.Multiline = true;
            this.tbLiveOutput.Name = "tbLiveOutput";
            this.tbLiveOutput.ReadOnly = true;
            this.tbLiveOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbLiveOutput.Size = new System.Drawing.Size(755, 345);
            this.tbLiveOutput.TabIndex = 0;
            this.tbLiveOutput.WordWrap = false;
            // 
            // cbUpdateOutput
            // 
            this.cbUpdateOutput.AutoSize = true;
            this.cbUpdateOutput.Checked = true;
            this.cbUpdateOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUpdateOutput.Location = new System.Drawing.Point(530, 419);
            this.cbUpdateOutput.Name = "cbUpdateOutput";
            this.cbUpdateOutput.Size = new System.Drawing.Size(96, 17);
            this.cbUpdateOutput.TabIndex = 4;
            this.cbUpdateOutput.Text = "Update Output";
            this.cbUpdateOutput.UseVisualStyleBackColor = true;
            this.cbUpdateOutput.CheckedChanged += new System.EventHandler(this.cbUpdateOutput_CheckedChanged);
            // 
            // TaskRunWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cbUpdateOutput);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnCancel);
            this.Name = "TaskRunWindow";
            this.Text = "LoggerWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskRunWindow_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbLiveOutput;
        private System.Windows.Forms.TextBox tbSummary;
        private System.Windows.Forms.CheckBox cbUpdateOutput;
    }
}