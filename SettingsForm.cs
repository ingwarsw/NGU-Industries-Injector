using System;
using System.Windows.Forms;
using System.Linq;
using System.ComponentModel;

namespace NGUIndustriesInjector
{
    public partial class SettingsForm : Form
    {
        private bool _initializing = true;

        public SettingsForm()
        {
            InitializeComponent();
            var version = typeof(Main).Assembly.GetName().Version.ToString();
            VersionLabel.Text = $"Version: {version}";

            FactoriesPrioListColumnName.DataSource = Enum.GetValues(typeof(BuildingType));
        }

        internal void UpdateFromSettings(SavedSettings newSettings)
        {
            _initializing = true;

            MasterEnable.Checked = newSettings.GlobalEnabled;
            AutoDailySpin.Checked = newSettings.AutoSpin;
            ManagePit.Checked = newSettings.ManagePit;
            FactoryDontStarve.Checked = newSettings.FactoryDontStarve;
            FactoryBuildStandard.Checked = newSettings.FactoryBuildStandard;
            ManageWorkOrders.Checked = newSettings.ManageWorkOrders;
            ManageFarmsCheckBox.Checked = newSettings.ManageFarms;
            ManageExperimentsCheckbox.Checked = newSettings.ManageExperiments;
            FreezeExperiments.Visible = Main.Settings.ManageExperiments;
            WeightedRewards.Visible = Main.Settings.ManageExperiments;
            FreezeExperiments.Checked = newSettings.FreezeExperiments;
            WeightedRewards.Checked = newSettings.WeightedRewards;
            ManageFactories.Checked = newSettings.ManageFactories;

            PitThreshold.Text = newSettings.PitThreshold.ToString();

            FactoryPriorityMaterialsDataGridView.DataSource = new BindingSource(new BindingList<PriorityMaterial>(Main.Settings.PriorityBuildings), null);

            Refresh();
            _initializing = false;
        }

        internal void UpdateProgressBar(int progress)
        {
            if (progress < 0)
                return;
            progressBar1.Value = progress;
        }

        private void MasterEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.GlobalEnabled = MasterEnable.Checked;
        }

        private void AutoDailySpin_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoSpin = AutoDailySpin.Checked;
        }

        private void AutoMoneyPit_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManagePit = ManagePit.Checked;
        }

        private void MoneyPitThresholdSave_Click(object sender, EventArgs e)
        {
            var newVal = PitThreshold.Text;
            if (double.TryParse(newVal, out var saved))
            {
                if (saved < 0)
                {
                    //moneyPitError.SetError(MoneyPitThreshold, "Not a valid value");
                    return;
                }
                Main.Settings.PitThreshold = saved;
            }
            else
            {
                //moneyPitError.SetError(MoneyPitThreshold, "Not a valid value");
            }
        }

        private void MoneyPitThreshold_TextChanged_1(object sender, EventArgs e)
        {
            // moneyPitError.SetError(MoneyPitThreshold, "");
        }

        private void UnloadSafety_CheckedChanged(object sender, EventArgs e)
        {
            UnloadButton.Enabled = UnloadSafety.Checked;
        }

        private void UnloadButton_Click(object sender, EventArgs e)
        {
            Loader.Unload();
        }

        private void FactoryDontStarve_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.FactoryDontStarve = FactoryDontStarve.Checked;
        }

        private void FactoryBuildStandard_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.FactoryBuildStandard = FactoryBuildStandard.Checked;
        }

        private void ManageWorkOrders_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageWorkOrders = ManageWorkOrders.Checked;
        }
        private void ManageFarmsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageFarms = ManageFarmsCheckBox.Checked;
        }

        private void FactoryPriorityItemsSaveButton_Click(object sender, EventArgs e)
        {
            Main.Settings.SaveSettings();
        }

        private void FactoryPriorityMaterialsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Main.Settings.SaveSettings();
        }

        private void ManageExperimentsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageExperiments = ManageExperimentsCheckbox.Checked;

            FreezeExperiments.Visible = Main.Settings.ManageExperiments;
            WeightedRewards.Visible = Main.Settings.ManageExperiments;
        }

        private void FreezeExperiments_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.FreezeExperiments = FreezeExperiments.Checked;
        }

        private void WeightedRewards_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.WeightedRewards = WeightedRewards.Checked;
        }

        private void ManageFactories_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.ManageFactories = ManageFactories.Checked;
        }
    }
}
