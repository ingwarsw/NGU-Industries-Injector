﻿using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NGUIndustriesInjector
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SavedSettings
    {
        [JsonProperty] private bool _globalEnabled = false;

        [JsonProperty] private bool _factoryDontStarve = true;
        [JsonProperty] private bool _factoryBuildStandard = true;
        [JsonProperty] private List<PriorityMaterial> _priorytyBuildings = new List<PriorityMaterial>();

        [JsonProperty] private bool _manageWorkOrders = false;
        [JsonProperty] private bool _manageFarms = false;
        [JsonProperty] private bool _autoSpin = false;

        [JsonProperty] private bool _managePit = false;
        [JsonProperty] private double _pitThreshold = 3600;

        private bool _disableSave;
        private readonly string savePath;

        public SavedSettings(string dir)
        {
            savePath = Path.Combine(dir, "settings.json");
        }

        internal void SaveSettings()
        {
            if (savePath == null) return;
            if (_disableSave) return;
            Main.Log("Saving Settings");
            Main.IgnoreNextChange = true;
            //var serialized = JsonUtility.ToJson(this, true);
            var serialized = JsonConvert.SerializeObject(this, Formatting.Indented);
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
                    _priorytyBuildings.Clear();
                    var json = File.ReadAllText(savePath);
                    JsonConvert.PopulateObject(json, this);
                    Main.Log("Loaded Settings");
                }
                catch (Exception e)
                {
                    Main.Log(e.Message);
                    Main.Log(e.StackTrace);
                    Main.Log("Creating new default Settings");
                }
            }
            else
            {
                Main.Log("Creating new default Settings");
            }
            //Main.Log(JsonUtility.ToJson(this, true));
            Main.Log(JsonConvert.SerializeObject(this, Formatting.Indented));
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

        public List<PriorityMaterial> PriorityBuildings
        {
            get => _priorytyBuildings;
            set
            {
                _priorytyBuildings = value;
                SaveSettings();
            }
        }

        public bool ManageWorkOrders
        {
            get => _manageWorkOrders;
            set
            {
                if (value == _manageWorkOrders) return;
                _manageWorkOrders = value;
                SaveSettings();
            }
        }

        public bool ManageFarms
        {
            get => _manageFarms;
            set
            {
                if (value == _manageFarms) return;
                _manageFarms = value;
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

        public bool ManagePit
        {
            get => _managePit;
            set
            {
                if (value == _managePit) return;
                _managePit = value;
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
