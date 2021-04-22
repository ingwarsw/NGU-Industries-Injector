namespace NGUIndustriesInjector
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.UnloadSafety = new System.Windows.Forms.CheckBox();
            this.UnloadButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.MoneyPitThresholdSave = new System.Windows.Forms.Button();
            this.MoneyPitThreshold = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MasterEnable = new System.Windows.Forms.CheckBox();
            this.AutoMoneyPit = new System.Windows.Forms.CheckBox();
            this.AutoDailySpin = new System.Windows.Forms.CheckBox();
            this.AutoITOPOD = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.progressBar1);
            this.flowLayoutPanel1.Controls.Add(this.tabControl1);
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.UnloadSafety);
            this.tabPage1.Controls.Add(this.UnloadButton);
            this.tabPage1.Controls.Add(this.VersionLabel);
            this.tabPage1.Controls.Add(this.MoneyPitThresholdSave);
            this.tabPage1.Controls.Add(this.MoneyPitThreshold);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.MasterEnable);
            this.tabPage1.Controls.Add(this.AutoMoneyPit);
            this.tabPage1.Controls.Add(this.AutoDailySpin);
            this.tabPage1.Controls.Add(this.AutoITOPOD);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // UnloadSafety
            // 
            resources.ApplyResources(this.UnloadSafety, "UnloadSafety");
            this.UnloadSafety.Name = "UnloadSafety";
            this.UnloadSafety.UseVisualStyleBackColor = true;
            this.UnloadSafety.CheckedChanged += new System.EventHandler(this.UnloadSafety_CheckedChanged);
            // 
            // UnloadButton
            // 
            resources.ApplyResources(this.UnloadButton, "UnloadButton");
            this.UnloadButton.Name = "UnloadButton";
            this.UnloadButton.UseVisualStyleBackColor = true;
            this.UnloadButton.Click += new System.EventHandler(this.UnloadButton_Click);
            // 
            // VersionLabel
            // 
            resources.ApplyResources(this.VersionLabel, "VersionLabel");
            this.VersionLabel.Name = "VersionLabel";
            // 
            // MoneyPitThresholdSave
            // 
            resources.ApplyResources(this.MoneyPitThresholdSave, "MoneyPitThresholdSave");
            this.MoneyPitThresholdSave.Name = "MoneyPitThresholdSave";
            this.MoneyPitThresholdSave.UseVisualStyleBackColor = true;
            this.MoneyPitThresholdSave.Click += new System.EventHandler(this.MoneyPitThresholdSave_Click);
            // 
            // MoneyPitThreshold
            // 
            resources.ApplyResources(this.MoneyPitThreshold, "MoneyPitThreshold");
            this.MoneyPitThreshold.Name = "MoneyPitThreshold";
            this.MoneyPitThreshold.TextChanged += new System.EventHandler(this.MoneyPitThreshold_TextChanged_1);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // MasterEnable
            // 
            resources.ApplyResources(this.MasterEnable, "MasterEnable");
            this.MasterEnable.Name = "MasterEnable";
            this.MasterEnable.UseVisualStyleBackColor = true;
            this.MasterEnable.CheckedChanged += new System.EventHandler(this.MasterEnable_CheckedChanged);
            // 
            // AutoMoneyPit
            // 
            resources.ApplyResources(this.AutoMoneyPit, "AutoMoneyPit");
            this.AutoMoneyPit.Name = "AutoMoneyPit";
            this.AutoMoneyPit.UseVisualStyleBackColor = true;
            this.AutoMoneyPit.CheckedChanged += new System.EventHandler(this.AutoMoneyPit_CheckedChanged);
            // 
            // AutoDailySpin
            // 
            resources.ApplyResources(this.AutoDailySpin, "AutoDailySpin");
            this.AutoDailySpin.Name = "AutoDailySpin";
            this.AutoDailySpin.UseVisualStyleBackColor = true;
            this.AutoDailySpin.CheckedChanged += new System.EventHandler(this.AutoDailySpin_CheckedChanged);
            // 
            // AutoITOPOD
            // 
            resources.ApplyResources(this.AutoITOPOD, "AutoITOPOD");
            this.AutoITOPOD.Name = "AutoITOPOD";
            this.AutoITOPOD.UseVisualStyleBackColor = true;
            this.AutoITOPOD.CheckedChanged += new System.EventHandler(this.AutoITOPOD_CheckedChanged);
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox UnloadSafety;
        private System.Windows.Forms.Button UnloadButton;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Button MoneyPitThresholdSave;
        private System.Windows.Forms.TextBox MoneyPitThreshold;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox MasterEnable;
        private System.Windows.Forms.CheckBox AutoMoneyPit;
        private System.Windows.Forms.CheckBox AutoDailySpin;
        private System.Windows.Forms.CheckBox AutoITOPOD;
    }
}