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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ManageWorkOrders = new System.Windows.Forms.CheckBox();
            this.UnloadSafety = new System.Windows.Forms.CheckBox();
            this.UnloadButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.PitThresholdSave = new System.Windows.Forms.Button();
            this.PitThreshold = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MasterEnable = new System.Windows.Forms.CheckBox();
            this.AutoPit = new System.Windows.Forms.CheckBox();
            this.AutoDailySpin = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.FactoryPriorityMaterialsDataGridView = new System.Windows.Forms.DataGridView();
            this.FactoryPriorityItemsLabelMain = new System.Windows.Forms.Label();
            this.FactoryPriorityItemsSaveButton = new System.Windows.Forms.Button();
            this.FactoryBuildStandard = new System.Windows.Forms.CheckBox();
            this.FactoryDontStarve = new System.Windows.Forms.CheckBox();
            this.FactoriesPrioListColumnName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FactoriesPrioListColumnWant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FactoryPriorityMaterialsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBar1.Location = new System.Drawing.Point(3, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(598, 13);
            this.progressBar1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.progressBar1);
            this.flowLayoutPanel1.Controls.Add(this.tabControl1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(605, 387);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 22);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(598, 360);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ManageWorkOrders);
            this.tabPage1.Controls.Add(this.UnloadSafety);
            this.tabPage1.Controls.Add(this.UnloadButton);
            this.tabPage1.Controls.Add(this.VersionLabel);
            this.tabPage1.Controls.Add(this.PitThresholdSave);
            this.tabPage1.Controls.Add(this.PitThreshold);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.MasterEnable);
            this.tabPage1.Controls.Add(this.AutoPit);
            this.tabPage1.Controls.Add(this.AutoDailySpin);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(590, 334);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ManageWorkOrders
            // 
            this.ManageWorkOrders.AutoSize = true;
            this.ManageWorkOrders.Location = new System.Drawing.Point(6, 52);
            this.ManageWorkOrders.Name = "ManageWorkOrders";
            this.ManageWorkOrders.Size = new System.Drawing.Size(128, 17);
            this.ManageWorkOrders.TabIndex = 18;
            this.ManageWorkOrders.Text = "Manage Work Orders";
            this.ManageWorkOrders.UseVisualStyleBackColor = true;
            this.ManageWorkOrders.CheckedChanged += new System.EventHandler(this.ManageWorkOrders_CheckedChanged);
            // 
            // UnloadSafety
            // 
            this.UnloadSafety.AutoSize = true;
            this.UnloadSafety.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UnloadSafety.Location = new System.Drawing.Point(396, 308);
            this.UnloadSafety.Name = "UnloadSafety";
            this.UnloadSafety.Size = new System.Drawing.Size(15, 14);
            this.UnloadSafety.TabIndex = 17;
            this.UnloadSafety.UseVisualStyleBackColor = true;
            this.UnloadSafety.CheckedChanged += new System.EventHandler(this.UnloadSafety_CheckedChanged);
            // 
            // UnloadButton
            // 
            this.UnloadButton.Enabled = false;
            this.UnloadButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UnloadButton.Location = new System.Drawing.Point(417, 303);
            this.UnloadButton.Name = "UnloadButton";
            this.UnloadButton.Size = new System.Drawing.Size(75, 23);
            this.UnloadButton.TabIndex = 16;
            this.UnloadButton.Text = "Unload";
            this.UnloadButton.UseVisualStyleBackColor = true;
            this.UnloadButton.Click += new System.EventHandler(this.UnloadButton_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.VersionLabel.Location = new System.Drawing.Point(498, 308);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(81, 13);
            this.VersionLabel.TabIndex = 15;
            this.VersionLabel.Text = "Version: 2.2.0.0";
            // 
            // PitThresholdSave
            // 
            this.PitThresholdSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PitThresholdSave.Location = new System.Drawing.Point(243, 147);
            this.PitThresholdSave.Name = "PitThresholdSave";
            this.PitThresholdSave.Size = new System.Drawing.Size(76, 20);
            this.PitThresholdSave.TabIndex = 12;
            this.PitThresholdSave.Text = "Save";
            this.PitThresholdSave.UseVisualStyleBackColor = true;
            this.PitThresholdSave.Visible = false;
            this.PitThresholdSave.Click += new System.EventHandler(this.MoneyPitThresholdSave_Click);
            // 
            // PitThreshold
            // 
            this.PitThreshold.Location = new System.Drawing.Point(113, 147);
            this.PitThreshold.Name = "PitThreshold";
            this.PitThreshold.Size = new System.Drawing.Size(124, 20);
            this.PitThreshold.TabIndex = 11;
            this.PitThreshold.Visible = false;
            this.PitThreshold.TextChanged += new System.EventHandler(this.MoneyPitThreshold_TextChanged_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(3, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Pit Threshold";
            this.label7.Visible = false;
            // 
            // MasterEnable
            // 
            this.MasterEnable.AutoSize = true;
            this.MasterEnable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MasterEnable.Location = new System.Drawing.Point(6, 6);
            this.MasterEnable.Name = "MasterEnable";
            this.MasterEnable.Size = new System.Drawing.Size(93, 17);
            this.MasterEnable.TabIndex = 4;
            this.MasterEnable.Text = "Master Switch";
            this.MasterEnable.UseVisualStyleBackColor = true;
            this.MasterEnable.CheckedChanged += new System.EventHandler(this.MasterEnable_CheckedChanged);
            // 
            // AutoPit
            // 
            this.AutoPit.AutoSize = true;
            this.AutoPit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AutoPit.Location = new System.Drawing.Point(5, 130);
            this.AutoPit.Name = "AutoPit";
            this.AutoPit.Size = new System.Drawing.Size(63, 17);
            this.AutoPit.TabIndex = 6;
            this.AutoPit.Text = "Auto Pit";
            this.AutoPit.UseVisualStyleBackColor = true;
            this.AutoPit.Visible = false;
            this.AutoPit.CheckedChanged += new System.EventHandler(this.AutoMoneyPit_CheckedChanged);
            // 
            // AutoDailySpin
            // 
            this.AutoDailySpin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AutoDailySpin.Location = new System.Drawing.Point(6, 29);
            this.AutoDailySpin.Name = "AutoDailySpin";
            this.AutoDailySpin.Size = new System.Drawing.Size(98, 17);
            this.AutoDailySpin.TabIndex = 5;
            this.AutoDailySpin.Text = "Auto Daily Spin";
            this.AutoDailySpin.UseVisualStyleBackColor = true;
            this.AutoDailySpin.CheckedChanged += new System.EventHandler(this.AutoDailySpin_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.FactoryPriorityMaterialsDataGridView);
            this.tabPage2.Controls.Add(this.FactoryPriorityItemsLabelMain);
            this.tabPage2.Controls.Add(this.FactoryPriorityItemsSaveButton);
            this.tabPage2.Controls.Add(this.FactoryBuildStandard);
            this.tabPage2.Controls.Add(this.FactoryDontStarve);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(590, 334);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Factories";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FactoryPriorityMaterialsDataGridView
            // 
            this.FactoryPriorityMaterialsDataGridView.AllowUserToResizeRows = false;
            this.FactoryPriorityMaterialsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FactoryPriorityMaterialsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FactoriesPrioListColumnName,
            this.FactoriesPrioListColumnWant});
            this.FactoryPriorityMaterialsDataGridView.Location = new System.Drawing.Point(6, 88);
            this.FactoryPriorityMaterialsDataGridView.MultiSelect = false;
            this.FactoryPriorityMaterialsDataGridView.Name = "FactoryPriorityMaterialsDataGridView";
            this.FactoryPriorityMaterialsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FactoryPriorityMaterialsDataGridView.Size = new System.Drawing.Size(559, 158);
            this.FactoryPriorityMaterialsDataGridView.TabIndex = 21;
            // 
            // FactoryPriorityItemsLabelMain
            // 
            this.FactoryPriorityItemsLabelMain.AutoSize = true;
            this.FactoryPriorityItemsLabelMain.Location = new System.Drawing.Point(6, 72);
            this.FactoryPriorityItemsLabelMain.Name = "FactoryPriorityItemsLabelMain";
            this.FactoryPriorityItemsLabelMain.Size = new System.Drawing.Size(53, 13);
            this.FactoryPriorityItemsLabelMain.TabIndex = 20;
            this.FactoryPriorityItemsLabelMain.Text = "Priority list";
            // 
            // FactoryPriorityItemsSaveButton
            // 
            this.FactoryPriorityItemsSaveButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FactoryPriorityItemsSaveButton.Location = new System.Drawing.Point(489, 252);
            this.FactoryPriorityItemsSaveButton.Name = "FactoryPriorityItemsSaveButton";
            this.FactoryPriorityItemsSaveButton.Size = new System.Drawing.Size(76, 20);
            this.FactoryPriorityItemsSaveButton.TabIndex = 13;
            this.FactoryPriorityItemsSaveButton.Text = "Save";
            this.FactoryPriorityItemsSaveButton.UseVisualStyleBackColor = true;
            this.FactoryPriorityItemsSaveButton.Click += new System.EventHandler(this.FactoryPriorityItemsSaveButton_Click);
            // 
            // FactoryBuildStandard
            // 
            this.FactoryBuildStandard.AutoSize = true;
            this.FactoryBuildStandard.Location = new System.Drawing.Point(7, 30);
            this.FactoryBuildStandard.Name = "FactoryBuildStandard";
            this.FactoryBuildStandard.Size = new System.Drawing.Size(322, 17);
            this.FactoryBuildStandard.TabIndex = 1;
            this.FactoryBuildStandard.Text = "Build \"standard\" number of all resources (with really low priority)";
            this.FactoryBuildStandard.UseVisualStyleBackColor = true;
            this.FactoryBuildStandard.CheckedChanged += new System.EventHandler(this.FactoryBuildStandard_CheckedChanged);
            // 
            // FactoryDontStarve
            // 
            this.FactoryDontStarve.AutoSize = true;
            this.FactoryDontStarve.Location = new System.Drawing.Point(7, 7);
            this.FactoryDontStarve.Name = "FactoryDontStarve";
            this.FactoryDontStarve.Size = new System.Drawing.Size(300, 17);
            this.FactoryDontStarve.TabIndex = 0;
            this.FactoryDontStarve.Text = "Dont allow starving of factories (never see red color again)";
            this.FactoryDontStarve.UseVisualStyleBackColor = true;
            this.FactoryDontStarve.CheckedChanged += new System.EventHandler(this.FactoryDontStarve_CheckedChanged);
            // 
            // FactoriesPrioListColumnName
            // 
            this.FactoriesPrioListColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FactoriesPrioListColumnName.DataPropertyName = "Type";
            this.FactoriesPrioListColumnName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.FactoriesPrioListColumnName.HeaderText = "Material";
            this.FactoriesPrioListColumnName.Name = "FactoriesPrioListColumnName";
            this.FactoriesPrioListColumnName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // FactoriesPrioListColumnWant
            // 
            this.FactoriesPrioListColumnWant.DataPropertyName = "Want";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.FactoriesPrioListColumnWant.DefaultCellStyle = dataGridViewCellStyle1;
            this.FactoriesPrioListColumnWant.HeaderText = "Number want";
            this.FactoriesPrioListColumnWant.Name = "FactoriesPrioListColumnWant";
            this.FactoriesPrioListColumnWant.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FactoriesPrioListColumnWant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FactoriesPrioListColumnWant.Width = 200;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 387);
            this.Controls.Add(this.flowLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "NGU INDUSTRIES Injector Settings";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FactoryPriorityMaterialsDataGridView)).EndInit();
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
        private System.Windows.Forms.Button PitThresholdSave;
        private System.Windows.Forms.TextBox PitThreshold;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox MasterEnable;
        private System.Windows.Forms.CheckBox AutoPit;
        private System.Windows.Forms.CheckBox AutoDailySpin;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox FactoryDontStarve;
        private System.Windows.Forms.CheckBox FactoryBuildStandard;
        private System.Windows.Forms.Button FactoryPriorityItemsSaveButton;
        private System.Windows.Forms.CheckBox ManageWorkOrders;
        private System.Windows.Forms.Label FactoryPriorityItemsLabelMain;
        private System.Windows.Forms.DataGridView FactoryPriorityMaterialsDataGridView;
        private System.Windows.Forms.DataGridViewComboBoxColumn FactoriesPrioListColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactoriesPrioListColumnWant;
    }
}