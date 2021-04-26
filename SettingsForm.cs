using System;
using System.Windows.Forms;

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
        }

        internal void UpdateFromSettings(SavedSettings newSettings)
        {
            _initializing = true;

            MasterEnable.Checked = newSettings.GlobalEnabled;
            AutoDailySpin.Checked = newSettings.AutoSpin;
            AutoPit.Checked = newSettings.AutoPit;
            FactoryDontStarve.Checked = newSettings.FactoryDontStarve;
            FactoryBuildStandard.Checked = newSettings.FactoryBuildStandard;

            PitThreshold.Text = $"{newSettings.PitThreshold:#.##E+00}";

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
            Main.Settings.AutoPit = AutoPit.Checked;
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
    }
}
