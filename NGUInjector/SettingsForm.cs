using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using NGUInjector.AllocationProfiles;
//using NGUInjector.Managers;

namespace NGUInjector
{
    public partial class SettingsForm : Form
    {
        internal readonly Dictionary<int, string> TitanList;
        internal readonly Dictionary<int, string> ZoneList;
        internal readonly Dictionary<int, string> CombatModeList;
        internal readonly Dictionary<int, string> CubePriorityList;
        private bool _initializing = true;
        public SettingsForm()
        {
            InitializeComponent();

            CubePriorityList = new Dictionary<int, string>();
            CombatModeList = new Dictionary<int, string>();
            ZoneList = new Dictionary<int, string>();
            TitanList = new Dictionary<int, string>();
            // Populate our data sources
            TitanList.Add(0, "None");
            TitanList.Add(1, "GRB");
            TitanList.Add(2, "GCT");
            TitanList.Add(3, "Jake");
            TitanList.Add(4, "UUG");
            TitanList.Add(5, "Walderp");
            TitanList.Add(6, "Beast");
            TitanList.Add(7, "Greasy Nerd");
            TitanList.Add(8, "Godmother");
            TitanList.Add(9, "Exile");
            TitanList.Add(10, "IT HUNGERS");
            TitanList.Add(11, "Rock Lobster");
            TitanList.Add(12, "Amalgamate");

            ZoneList.Add(-1, "Safe Zone: Awakening Site");
            ZoneList.Add(0, "Tutorial Zone");
            ZoneList.Add(1, "Sewers");
            ZoneList.Add(2, "Forest");
            ZoneList.Add(3, "Cave of Many Things");
            ZoneList.Add(4, "The Sky");
            ZoneList.Add(5, "High Security Base");
            ZoneList.Add(6, "Gordon Ramsay Bolton");
            ZoneList.Add(7, "Clock Dimension");
            ZoneList.Add(8, "Grand Corrupted Tree");
            ZoneList.Add(9, "The 2D Universe");
            ZoneList.Add(10, "Ancient Battlefield");
            ZoneList.Add(11, "Jake From Accounting");
            ZoneList.Add(12, "A Very Strange Place");
            ZoneList.Add(13, "Mega Lands");
            ZoneList.Add(14, "UUG THE UNMENTIONABLE");
            ZoneList.Add(15, "The Beardverse");
            ZoneList.Add(16, "WALDERP");
            ZoneList.Add(17, "Badly Drawn World");
            ZoneList.Add(18, "Boring-Ass Earth");
            ZoneList.Add(19, "THE BEAST");
            ZoneList.Add(20, "Chocolate World");
            ZoneList.Add(21, "The Evilverse");
            ZoneList.Add(22, "Pretty Pink Princess Land");
            ZoneList.Add(23, "GREASY NERD");
            ZoneList.Add(24, "Meta Land");
            ZoneList.Add(25, "Interdimensional Party");
            ZoneList.Add(26, "THE GODMOTHER");
            ZoneList.Add(27, "Typo Zonw");
            ZoneList.Add(28, "The Fad-Lands");
            ZoneList.Add(29, "JRPGVille");
            ZoneList.Add(30, "THE EXILE");
            ZoneList.Add(31, "The Rad-lands");
            ZoneList.Add(32, "Back To School");
            ZoneList.Add(33, "The West World");
            ZoneList.Add(34, "IT HUNGERS");
            ZoneList.Add(35, "The Breadverse");
            ZoneList.Add(36, "That 70's Zone");
            ZoneList.Add(37, "The Halloweenies");
            ZoneList.Add(38, "ROCK LOBSTER");
            ZoneList.Add(39, "Construction Zone");
            ZoneList.Add(40, "DUCK DUCK ZONE");
            ZoneList.Add(41, "The Nether Regions");
            ZoneList.Add(42, "AMALGAMATE");
            ZoneList.Add(1000, "ITOPOD");

            CombatModeList.Add(0, "Manual");
            CombatModeList.Add(1, "Idle");

            CubePriorityList.Add(0, "None");
            CubePriorityList.Add(1, "Balanced");
            CubePriorityList.Add(2, "Power");
            CubePriorityList.Add(3, "Toughness");

            //Remove ITOPOD for non combat zones
            ZoneList.Remove(1000);

            VersionLabel.Text = $"Version: {Main.Version}";
        }
        
        internal void UpdateFromSettings(SavedSettings newSettings)
        {
            _initializing = true;
            AutoDailySpin.Checked = newSettings.AutoSpin;
            AutoITOPOD.Checked = newSettings.AutoQuestITOPOD;
            AutoMoneyPit.Checked = newSettings.AutoMoneyPit;
            MoneyPitThreshold.Text = $"{newSettings.MoneyPitThreshold:#.##E+00}"; 

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
            Main.Settings.AutoMoneyPit = AutoMoneyPit.Checked;
        }

        private void AutoITOPOD_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Main.Settings.AutoQuestITOPOD = AutoITOPOD.Checked;
        }

        private void MoneyPitThresholdSave_Click(object sender, EventArgs e)
        {
            var newVal = MoneyPitThreshold.Text;
            if (double.TryParse(newVal, out var saved))
            {
                if (saved < 0)
                {
                    //moneyPitError.SetError(MoneyPitThreshold, "Not a valid value");
                    return;
                }
                Main.Settings.MoneyPitThreshold = saved;
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
    }
}
