using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace NGUIndustriesInjector
{
     [Serializable]
    public class SavedSettings
    {
        [SerializeField] private int _highestAkZone;
        [SerializeField] private bool _autoQuestItopod;
        [SerializeField] private bool _autoMoneyPit;
        [SerializeField] private bool _autoSpin;
        [SerializeField] private double _moneyPitThreshold;
        [SerializeField] private bool _globalEnabled;

        private bool _disableSave;
        private string savePath;
        
         internal void SaveSettings()
        {
            if (savePath == null) return;
            if (_disableSave) return;
            Main.Log("Saving Settings");
            Main.IgnoreNextChange = true;
            var serialized = JsonUtility.ToJson(this, true);
            using (var writer = new StreamWriter(savePath))
            {
                writer.Write(serialized);
                writer.Flush();
            }
            Main.UpdateForm(this);
        }

        internal void SetSaveDisabled(bool disabled)
        {
            _disableSave = disabled;
        }

        internal static SavedSettings LoadSettings(string dir)
        {
            var savePath = Path.Combine(dir, "settings.json");
            if (File.Exists(savePath))
            {
                try
                {
                    var loadedSettings = JsonUtility.FromJson<SavedSettings>(File.ReadAllText(savePath));
                    Main.Log("Loaded Settings");
                    Main.Log(JsonUtility.ToJson(loadedSettings, true));
                    loadedSettings.savePath = savePath;
                    return loadedSettings;
                }
                catch (Exception e)
                {
                    Main.Log(e.Message);
                    Main.Log(e.StackTrace);
                }
            }
            Main.Log("Creating new default Settings");
            var newSettings = new SavedSettings
            {
                savePath = savePath
            };
            return newSettings;
        }

        public int HighestAKZone
        {
            get => _highestAkZone;
            set
            {
                _highestAkZone = value;
                SaveSettings();
            }
        }

        public bool AutoQuestITOPOD
        {
            get => _autoQuestItopod;
            set
            {
                if (value == _autoQuestItopod) return;
                _autoQuestItopod = value;
                SaveSettings();
            }
        }

        public bool AutoMoneyPit
        {
            get => _autoMoneyPit;
            set
            {
                if (value == _autoMoneyPit) return;
                _autoMoneyPit = value;
                SaveSettings();
            }
        }

        public bool AutoSpin
        {
            get => _autoSpin;
            set
            {
                if (value == _autoSpin) return;
                _autoSpin = value;
                SaveSettings();
            }
        }


        public double MoneyPitThreshold
        {
            get => _moneyPitThreshold;
            set
            {
                _moneyPitThreshold = value;
                SaveSettings();
            }
        }


        public bool GlobalEnabled
        {
            get => _globalEnabled;
            set
            {
                if (value == _globalEnabled) return;
                _globalEnabled = value;
                SaveSettings();
            }
        }
    }
}
