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
            this.ManageFactories = new System.Windows.Forms.CheckBox();
            this.WeightedRewards = new System.Windows.Forms.CheckBox();
            this.FreezeExperiments = new System.Windows.Forms.CheckBox();
            this.ManageExperimentsCheckbox = new System.Windows.Forms.CheckBox();
            this.ManageFarmsCheckBox = new System.Windows.Forms.CheckBox();
            this.ManageWorkOrders = new System.Windows.Forms.CheckBox();
            this.UnloadSafety = new System.Windows.Forms.CheckBox();
            this.UnloadButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.PitThresholdSave = new System.Windows.Forms.Button();
            this.PitThreshold = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.MasterEnable = new System.Windows.Forms.CheckBox();
            this.ManagePit = new System.Windows.Forms.CheckBox();
            this.AutoDailySpin = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.FactoryPriorityMaterialsDataGridView = new System.Windows.Forms.DataGridView();
            this.FactoryPriorityItemsLabelMain = new System.Windows.Forms.Label();
            this.FactoryPriorityItemsSaveButton = new System.Windows.Forms.Button();
            this.FactoryBuildStandard = new System.Windows.Forms.CheckBox();
            this.FactoryDontStarve = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.GlobalBluePrintsDataView = new System.Windows.Forms.DataGridView();
            this.blueprintName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blueprintSave = new System.Windows.Forms.DataGridViewButtonColumn();
            this.blueprintLoad = new System.Windows.Forms.DataGridViewButtonColumn();
            this.FactoriesPrioListColumnName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FactoriesPrioListColumnWant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FactoryPriorityMaterialsDataGridView)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalBluePrintsDataView)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBar1.Location = new System.Drawing.Point(4, 5);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(897, 20);
            this.progressBar1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.progressBar1);
            this.flowLayoutPanel1.Controls.Add(this.tabControl1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(908, 595);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(4, 35);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(897, 554);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ManageFactories);
            this.tabPage1.Controls.Add(this.WeightedRewards);
            this.tabPage1.Controls.Add(this.FreezeExperiments);
            this.tabPage1.Controls.Add(this.ManageExperimentsCheckbox);
            this.tabPage1.Controls.Add(this.ManageFarmsCheckBox);
            this.tabPage1.Controls.Add(this.ManageWorkOrders);
            this.tabPage1.Controls.Add(this.UnloadSafety);
            this.tabPage1.Controls.Add(this.UnloadButton);
            this.tabPage1.Controls.Add(this.VersionLabel);
            this.tabPage1.Controls.Add(this.PitThresholdSave);
            this.tabPage1.Controls.Add(this.PitThreshold);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.MasterEnable);
            this.tabPage1.Controls.Add(this.ManagePit);
            this.tabPage1.Controls.Add(this.AutoDailySpin);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(889, 521);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General Settings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ManageFactories
            // 
            this.ManageFactories.AutoSize = true;
            this.ManageFactories.Location = new System.Drawing.Point(200, 46);
            this.ManageFactories.Name = "ManageFactories";
            this.ManageFactories.Size = new System.Drawing.Size(163, 24);
            this.ManageFactories.TabIndex = 23;
            this.ManageFactories.Text = "Manage Factories";
            this.ManageFactories.UseVisualStyleBackColor = true;
            this.ManageFactories.CheckedChanged += new System.EventHandler(this.ManageFactories_CheckedChanged);
            // 
            // WeightedRewards
            // 
            this.WeightedRewards.AutoSize = true;
            this.WeightedRewards.Location = new System.Drawing.Point(384, 148);
            this.WeightedRewards.Name = "WeightedRewards";
            this.WeightedRewards.Size = new System.Drawing.Size(170, 24);
            this.WeightedRewards.TabIndex = 22;
            this.WeightedRewards.Text = "Weighted Rewards";
            this.WeightedRewards.UseVisualStyleBackColor = true;
            this.WeightedRewards.Visible = false;
            this.WeightedRewards.CheckedChanged += new System.EventHandler(this.WeightedRewards_CheckedChanged);
            // 
            // FreezeExperiments
            // 
            this.FreezeExperiments.AutoSize = true;
            this.FreezeExperiments.Location = new System.Drawing.Point(200, 148);
            this.FreezeExperiments.Name = "FreezeExperiments";
            this.FreezeExperiments.Size = new System.Drawing.Size(177, 24);
            this.FreezeExperiments.TabIndex = 21;
            this.FreezeExperiments.Text = "Freeze Experiments";
            this.FreezeExperiments.UseVisualStyleBackColor = true;
            this.FreezeExperiments.Visible = false;
            this.FreezeExperiments.CheckedChanged += new System.EventHandler(this.FreezeExperiments_CheckedChanged);
            // 
            // ManageExperimentsCheckbox
            // 
            this.ManageExperimentsCheckbox.AutoSize = true;
            this.ManageExperimentsCheckbox.Location = new System.Drawing.Point(9, 148);
            this.ManageExperimentsCheckbox.Name = "ManageExperimentsCheckbox";
            this.ManageExperimentsCheckbox.Size = new System.Drawing.Size(185, 24);
            this.ManageExperimentsCheckbox.TabIndex = 20;
            this.ManageExperimentsCheckbox.Text = "Manage Experiments";
            this.ManageExperimentsCheckbox.UseVisualStyleBackColor = true;
            this.ManageExperimentsCheckbox.CheckedChanged += new System.EventHandler(this.ManageExperimentsCheckbox_CheckedChanged);
            // 
            // ManageFarmsCheckBox
            // 
            this.ManageFarmsCheckBox.AutoSize = true;
            this.ManageFarmsCheckBox.Location = new System.Drawing.Point(9, 115);
            this.ManageFarmsCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ManageFarmsCheckBox.Name = "ManageFarmsCheckBox";
            this.ManageFarmsCheckBox.Size = new System.Drawing.Size(142, 24);
            this.ManageFarmsCheckBox.TabIndex = 19;
            this.ManageFarmsCheckBox.Text = "Manage Farms";
            this.ManageFarmsCheckBox.UseVisualStyleBackColor = true;
            this.ManageFarmsCheckBox.CheckedChanged += new System.EventHandler(this.ManageFarmsCheckBox_CheckedChanged);
            // 
            // ManageWorkOrders
            // 
            this.ManageWorkOrders.AutoSize = true;
            this.ManageWorkOrders.Location = new System.Drawing.Point(9, 80);
            this.ManageWorkOrders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ManageWorkOrders.Name = "ManageWorkOrders";
            this.ManageWorkOrders.Size = new System.Drawing.Size(186, 24);
            this.ManageWorkOrders.TabIndex = 18;
            this.ManageWorkOrders.Text = "Manage Work Orders";
            this.ManageWorkOrders.UseVisualStyleBackColor = true;
            this.ManageWorkOrders.CheckedChanged += new System.EventHandler(this.ManageWorkOrders_CheckedChanged);
            // 
            // UnloadSafety
            // 
            this.UnloadSafety.AutoSize = true;
            this.UnloadSafety.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UnloadSafety.Location = new System.Drawing.Point(594, 474);
            this.UnloadSafety.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UnloadSafety.Name = "UnloadSafety";
            this.UnloadSafety.Size = new System.Drawing.Size(22, 21);
            this.UnloadSafety.TabIndex = 17;
            this.UnloadSafety.UseVisualStyleBackColor = true;
            this.UnloadSafety.CheckedChanged += new System.EventHandler(this.UnloadSafety_CheckedChanged);
            // 
            // UnloadButton
            // 
            this.UnloadButton.Enabled = false;
            this.UnloadButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.UnloadButton.Location = new System.Drawing.Point(626, 466);
            this.UnloadButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UnloadButton.Name = "UnloadButton";
            this.UnloadButton.Size = new System.Drawing.Size(112, 35);
            this.UnloadButton.TabIndex = 16;
            this.UnloadButton.Text = "Unload";
            this.UnloadButton.UseVisualStyleBackColor = true;
            this.UnloadButton.Click += new System.EventHandler(this.UnloadButton_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.VersionLabel.Location = new System.Drawing.Point(747, 474);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(119, 20);
            this.VersionLabel.TabIndex = 15;
            this.VersionLabel.Text = "Version: 2.2.0.0";
            // 
            // PitThresholdSave
            // 
            this.PitThresholdSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PitThresholdSave.Location = new System.Drawing.Point(402, 226);
            this.PitThresholdSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PitThresholdSave.Name = "PitThresholdSave";
            this.PitThresholdSave.Size = new System.Drawing.Size(114, 31);
            this.PitThresholdSave.TabIndex = 12;
            this.PitThresholdSave.Text = "Save";
            this.PitThresholdSave.UseVisualStyleBackColor = true;
            this.PitThresholdSave.Click += new System.EventHandler(this.MoneyPitThresholdSave_Click);
            // 
            // PitThreshold
            // 
            this.PitThreshold.Location = new System.Drawing.Point(207, 226);
            this.PitThreshold.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PitThreshold.Name = "PitThreshold";
            this.PitThreshold.Size = new System.Drawing.Size(184, 26);
            this.PitThreshold.TabIndex = 11;
            this.PitThreshold.TextChanged += new System.EventHandler(this.MoneyPitThreshold_TextChanged_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(9, 231);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(191, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Pit Threshold (in seconds)";
            // 
            // MasterEnable
            // 
            this.MasterEnable.AutoSize = true;
            this.MasterEnable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.MasterEnable.Location = new System.Drawing.Point(9, 9);
            this.MasterEnable.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MasterEnable.Name = "MasterEnable";
            this.MasterEnable.Size = new System.Drawing.Size(135, 24);
            this.MasterEnable.TabIndex = 4;
            this.MasterEnable.Text = "Master Switch";
            this.MasterEnable.UseVisualStyleBackColor = true;
            this.MasterEnable.CheckedChanged += new System.EventHandler(this.MasterEnable_CheckedChanged);
            // 
            // ManagePit
            // 
            this.ManagePit.AutoSize = true;
            this.ManagePit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ManagePit.Location = new System.Drawing.Point(9, 200);
            this.ManagePit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ManagePit.Name = "ManagePit";
            this.ManagePit.Size = new System.Drawing.Size(115, 24);
            this.ManagePit.TabIndex = 6;
            this.ManagePit.Text = "Manage Pit";
            this.ManagePit.UseVisualStyleBackColor = true;
            this.ManagePit.CheckedChanged += new System.EventHandler(this.AutoMoneyPit_CheckedChanged);
            // 
            // AutoDailySpin
            // 
            this.AutoDailySpin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.AutoDailySpin.Location = new System.Drawing.Point(9, 45);
            this.AutoDailySpin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AutoDailySpin.Name = "AutoDailySpin";
            this.AutoDailySpin.Size = new System.Drawing.Size(147, 26);
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
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(889, 521);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Factories";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FactoryPriorityMaterialsDataGridView
            // 
            this.FactoryPriorityMaterialsDataGridView.AccessibleRole = System.Windows.Forms.AccessibleRole.Alert;
            this.FactoryPriorityMaterialsDataGridView.AllowUserToResizeRows = false;
            this.FactoryPriorityMaterialsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FactoryPriorityMaterialsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FactoriesPrioListColumnName,
            this.FactoriesPrioListColumnWant});
            this.FactoryPriorityMaterialsDataGridView.Location = new System.Drawing.Point(9, 135);
            this.FactoryPriorityMaterialsDataGridView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FactoryPriorityMaterialsDataGridView.MultiSelect = false;
            this.FactoryPriorityMaterialsDataGridView.Name = "FactoryPriorityMaterialsDataGridView";
            this.FactoryPriorityMaterialsDataGridView.RowHeadersWidth = 62;
            this.FactoryPriorityMaterialsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FactoryPriorityMaterialsDataGridView.Size = new System.Drawing.Size(838, 243);
            this.FactoryPriorityMaterialsDataGridView.TabIndex = 21;
            // 
            // FactoryPriorityItemsLabelMain
            // 
            this.FactoryPriorityItemsLabelMain.AutoSize = true;
            this.FactoryPriorityItemsLabelMain.Location = new System.Drawing.Point(9, 111);
            this.FactoryPriorityItemsLabelMain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FactoryPriorityItemsLabelMain.Name = "FactoryPriorityItemsLabelMain";
            this.FactoryPriorityItemsLabelMain.Size = new System.Drawing.Size(79, 20);
            this.FactoryPriorityItemsLabelMain.TabIndex = 20;
            this.FactoryPriorityItemsLabelMain.Text = "Priority list";
            // 
            // FactoryPriorityItemsSaveButton
            // 
            this.FactoryPriorityItemsSaveButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FactoryPriorityItemsSaveButton.Location = new System.Drawing.Point(734, 388);
            this.FactoryPriorityItemsSaveButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FactoryPriorityItemsSaveButton.Name = "FactoryPriorityItemsSaveButton";
            this.FactoryPriorityItemsSaveButton.Size = new System.Drawing.Size(114, 31);
            this.FactoryPriorityItemsSaveButton.TabIndex = 13;
            this.FactoryPriorityItemsSaveButton.Text = "Save";
            this.FactoryPriorityItemsSaveButton.UseVisualStyleBackColor = true;
            this.FactoryPriorityItemsSaveButton.Click += new System.EventHandler(this.FactoryPriorityItemsSaveButton_Click);
            // 
            // FactoryBuildStandard
            // 
            this.FactoryBuildStandard.AutoSize = true;
            this.FactoryBuildStandard.Location = new System.Drawing.Point(10, 46);
            this.FactoryBuildStandard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FactoryBuildStandard.Name = "FactoryBuildStandard";
            this.FactoryBuildStandard.Size = new System.Drawing.Size(477, 24);
            this.FactoryBuildStandard.TabIndex = 1;
            this.FactoryBuildStandard.Text = "Build \"standard\" number of all resources (with really low priority)";
            this.FactoryBuildStandard.UseVisualStyleBackColor = true;
            this.FactoryBuildStandard.CheckedChanged += new System.EventHandler(this.FactoryBuildStandard_CheckedChanged);
            // 
            // FactoryDontStarve
            // 
            this.FactoryDontStarve.AutoSize = true;
            this.FactoryDontStarve.Location = new System.Drawing.Point(10, 11);
            this.FactoryDontStarve.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FactoryDontStarve.Name = "FactoryDontStarve";
            this.FactoryDontStarve.Size = new System.Drawing.Size(442, 24);
            this.FactoryDontStarve.TabIndex = 0;
            this.FactoryDontStarve.Text = "Dont allow starving of factories (never see red color again)";
            this.FactoryDontStarve.UseVisualStyleBackColor = true;
            this.FactoryDontStarve.CheckedChanged += new System.EventHandler(this.FactoryDontStarve_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.GlobalBluePrintsDataView);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(889, 521);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Global Blueprints";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(760, 339);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 31);
            this.button2.TabIndex = 14;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Global Blueprints";
            // 
            // GlobalBluePrintsDataView
            // 
            this.GlobalBluePrintsDataView.ColumnHeadersHeight = 34;
            this.GlobalBluePrintsDataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.blueprintName,
            this.blueprintSave,
            this.blueprintLoad});
            this.GlobalBluePrintsDataView.Location = new System.Drawing.Point(17, 45);
            this.GlobalBluePrintsDataView.Name = "GlobalBluePrintsDataView";
            this.GlobalBluePrintsDataView.RowHeadersWidth = 62;
            this.GlobalBluePrintsDataView.RowTemplate.Height = 28;
            this.GlobalBluePrintsDataView.Size = new System.Drawing.Size(857, 286);
            this.GlobalBluePrintsDataView.TabIndex = 0;
            this.GlobalBluePrintsDataView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GlobalBluePrintsDataView_CellContentClick);
            this.GlobalBluePrintsDataView.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GlobalBluePrintsDataView_RowValidating);
            // 
            // blueprintName
            // 
            this.blueprintName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.blueprintName.DataPropertyName = "Name";
            this.blueprintName.HeaderText = "Name";
            this.blueprintName.MinimumWidth = 8;
            this.blueprintName.Name = "blueprintName";
            this.blueprintName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // blueprintSave
            // 
            this.blueprintSave.HeaderText = "";
            this.blueprintSave.MinimumWidth = 8;
            this.blueprintSave.Name = "blueprintSave";
            this.blueprintSave.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.blueprintSave.Text = "Save";
            this.blueprintSave.UseColumnTextForButtonValue = true;
            this.blueprintSave.Width = 150;
            // 
            // blueprintLoad
            // 
            this.blueprintLoad.HeaderText = "";
            this.blueprintLoad.MinimumWidth = 8;
            this.blueprintLoad.Name = "blueprintLoad";
            this.blueprintLoad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.blueprintLoad.Text = "Load";
            this.blueprintLoad.UseColumnTextForButtonValue = true;
            this.blueprintLoad.Width = 150;
            // 
            // FactoriesPrioListColumnName
            // 
            this.FactoriesPrioListColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FactoriesPrioListColumnName.DataPropertyName = "Type";
            this.FactoriesPrioListColumnName.HeaderText = "Material";
            this.FactoriesPrioListColumnName.MinimumWidth = 80;
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
            this.FactoriesPrioListColumnWant.MinimumWidth = 8;
            this.FactoriesPrioListColumnWant.Name = "FactoriesPrioListColumnWant";
            this.FactoriesPrioListColumnWant.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FactoriesPrioListColumnWant.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FactoriesPrioListColumnWant.Width = 200;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 595);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalBluePrintsDataView)).EndInit();
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
        private System.Windows.Forms.CheckBox ManagePit;
        private System.Windows.Forms.CheckBox AutoDailySpin;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox FactoryDontStarve;
        private System.Windows.Forms.CheckBox FactoryBuildStandard;
        private System.Windows.Forms.Button FactoryPriorityItemsSaveButton;
        private System.Windows.Forms.CheckBox ManageWorkOrders;
        private System.Windows.Forms.Label FactoryPriorityItemsLabelMain;
        private System.Windows.Forms.DataGridView FactoryPriorityMaterialsDataGridView;
        private System.Windows.Forms.CheckBox ManageFarmsCheckBox;
        private System.Windows.Forms.CheckBox ManageExperimentsCheckbox;
        private System.Windows.Forms.CheckBox WeightedRewards;
        private System.Windows.Forms.CheckBox FreezeExperiments;
        private System.Windows.Forms.CheckBox ManageFactories;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView GlobalBluePrintsDataView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn blueprintName;
        private System.Windows.Forms.DataGridViewButtonColumn blueprintSave;
        private System.Windows.Forms.DataGridViewButtonColumn blueprintLoad;
        private System.Windows.Forms.DataGridViewComboBoxColumn FactoriesPrioListColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactoriesPrioListColumnWant;
    }
}