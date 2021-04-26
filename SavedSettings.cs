using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NGUIndustriesInjector
{
    [Serializable]
    public class SavedSettings
    {
        [SerializeField] private bool _globalEnabled = false;

        [SerializeField] private bool _factoryDontStarve = true;
        [SerializeField] private bool _factoryBuildStandard = true;
        public List<Vector2> _priorytyBuildings = new List<Vector2>();

        [SerializeField] private bool _autoSpin = false;

        [SerializeField] private bool _autoPit = false;
        [SerializeField] private double _pitThreshold = 10000000000000;



        private bool _disableSave;
        private readonly string savePath;


        internal SavedSettings(string dir)
        {
            savePath = Path.Combine(dir, "settings.json");
        }

        internal void SaveSettings()
        {
            if (savePath == null) return;
            if (_disableSave) return;
            Main.Log("Saving Settings");
            Main.IgnoreNextChange = true;
            var serialized = JsonUtility.ToJson(this, true);
            //var serialized = fastJSON.JSON.ToNiceJSON(this);
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

        internal void LoadSettings()
        {
            if (File.Exists(savePath))
            {
                try
                {
                    var json = File.ReadAllText(savePath);
                    //fastJSON.JSON.FillObject(this, json);
                    JsonUtility.FromJsonOverwrite(json, this);
                    Main.Log("Loaded Settings");
                }
                catch (Exception e)
                {
                    Main.Log(e.Message);
                    Main.Log(e.StackTrace);
                }
            }
            else
            {
                Main.Log("Creating new default Settings");
            }
            //Main.Log(fastJSON.JSON.ToNiceJSON(this));
            Main.Log(JsonUtility.ToJson(this, true));
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
        public bool FactoryDontStarve
        {
            get => _factoryDontStarve;
            set
            {
                if (value == _factoryDontStarve) return;
                _factoryDontStarve = value;
                SaveSettings();
            }
        }

        public bool FactoryBuildStandard
        {
            get => _factoryBuildStandard;
            set
            {
                if (value == _factoryBuildStandard) return;
                _factoryBuildStandard = value;
                SaveSettings();
            }
        }

        public List<Vector2> PriorityBuildings
        {
            get => _priorytyBuildings;
            set
            {
                _priorytyBuildings = value;
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

        public bool AutoPit
        {
            get => _autoPit;
            set
            {
                if (value == _autoPit) return;
                _autoPit = value;
                SaveSettings();
            }
        }


        public double PitThreshold
        {
            get => _pitThreshold;
            set
            {
                _pitThreshold = value;
                SaveSettings();
            }
        }


    }
}
