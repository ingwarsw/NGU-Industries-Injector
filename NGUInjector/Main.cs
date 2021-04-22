using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace NGUInjector
{
    internal class Main : MonoBehaviour
    {
        private const float MAIN_DELAY = 60;
        internal static StreamWriter OutputWriter;
        internal static StreamWriter LootWriter;
        internal static StreamWriter CombatWriter;
        internal static StreamWriter PitSpinWriter;
        internal static Main reference;
        private float _timeLeft = MAIN_DELAY;
        internal static SettingsForm settingsForm;
        internal const string Version = "1.0.6";

        private static string _dir;
        private static string _profilesDir;

        internal static FileSystemWatcher ConfigWatcher;

        internal static bool IgnoreNextChange { get; set; }

        internal static SavedSettings Settings;

        internal static void Log(string msg)
        {
            OutputWriter.WriteLine($"{ DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()}: {msg}");
        }

        internal static void LogLoot(string msg)
        {
            LootWriter.WriteLine($"{ DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()}: {msg}");
        }

        internal static void LogCombat(string msg)
        {
            CombatWriter.WriteLine($"{DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()}: {msg}");
        }

        internal static void LogPitSpin(string msg)
        {
            PitSpinWriter.WriteLine($"{DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()}: {msg}");
        }

        internal void Unload()
        {
            try
            {
                Log("Unloading injector");
                CancelInvoke("AutomationRoutine");
                CancelInvoke("SnipeZone");
                CancelInvoke("MonitorLog");
                CancelInvoke("QuickStuff");
                CancelInvoke("SetResnipe");
                CancelInvoke("ShowBoostProgress");


                LootWriter.Close();
                CombatWriter.Close();
                PitSpinWriter.Close();
                settingsForm.Close();
                settingsForm.Dispose();

                ConfigWatcher.Dispose();
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
            OutputWriter.Close();
        }

        public void Start()
        {
            try
            {
                _dir = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%/Desktop"), "NGUIndustriesInjector");
                if (!Directory.Exists(_dir))
                {
                    Directory.CreateDirectory(_dir);
                }

                var logDir = Path.Combine(_dir, "logs");
                if (!Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }

                OutputWriter = new StreamWriter(Path.Combine(logDir, "inject.log")) { AutoFlush = true };
                LootWriter = new StreamWriter(Path.Combine(logDir, "loot.log")) { AutoFlush = true };
                CombatWriter = new StreamWriter(Path.Combine(logDir, "combat.log")) { AutoFlush = true };
                PitSpinWriter = new StreamWriter(Path.Combine(logDir, "pitspin.log"), true) { AutoFlush = true };

                _profilesDir = Path.Combine(_dir, "profiles");
                if (!Directory.Exists(_profilesDir))
                {
                    Directory.CreateDirectory(_profilesDir);
                }

                var oldPath = Path.Combine(_dir, "allocation.json");
                var newPath = Path.Combine(_profilesDir, "default.json");

                if (File.Exists(oldPath) && !File.Exists(newPath))
                {
                    File.Move(oldPath, newPath);
                }
            }
            catch (Exception e)
            {
                Log(e.Message);
                Log(e.StackTrace);
                Loader.Unload();
                return;
            }

            try
            {
                Log("Injected");
                LogLoot("Starting Loot Writer");
                LogCombat("Starting Combat Writer");

                Settings = new SavedSettings(_dir);

                if (!Settings.LoadSettings())
                {
                    //Settings.CreateDefault();
                    Log($"Created default settings");
                }

                settingsForm = new SettingsForm();

                ConfigWatcher = new FileSystemWatcher
                {
                    Path = _dir,
                    Filter = "settings.json",
                    NotifyFilter = NotifyFilters.LastWrite,
                    EnableRaisingEvents = true
                };

                ConfigWatcher.Changed += (sender, args) =>
                {
                    if (IgnoreNextChange)
                    {
                        IgnoreNextChange = false;
                        return;
                    }
                    Settings.LoadSettings();
                    settingsForm.UpdateFromSettings(Settings);
                };

                Settings.SaveSettings();
                Settings.LoadSettings();

                settingsForm.UpdateFromSettings(Settings);
                settingsForm.Show();

                InvokeRepeating("AutomationRoutine", 0.0f, MAIN_DELAY);
                InvokeRepeating("SnipeZone", 0.0f, .1f);
                InvokeRepeating("MonitorLog", 0.0f, 1f);
                InvokeRepeating("QuickStuff", 0.0f, .5f);
                InvokeRepeating("ShowBoostProgress", 0.0f, 60.0f);
                InvokeRepeating("SetResnipe", 0f, 1f);

                reference = this;
            }
            catch (Exception e)
            {
                Log(e.ToString());
                Log(e.StackTrace);
                Log(e.InnerException.ToString());
            }
        }

        internal static void UpdateForm(SavedSettings newSettings)
        {
            settingsForm.UpdateFromSettings(newSettings);
        }

        public void Update()
        {
            _timeLeft -= Time.deltaTime;

            settingsForm.UpdateProgressBar((int)Math.Floor(_timeLeft / MAIN_DELAY * 100));

            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (!settingsForm.Visible)
                {
                    settingsForm.Show();
                }
                settingsForm.BringToFront();
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                Settings.GlobalEnabled = !Settings.GlobalEnabled;
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                QuickSave();
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                QuickLoad();
            }
        }

        private void QuickSave()
        {
            Log("Writing quicksave and json");
            //var data = Character.importExport.getBase64Data();
            //using (var writer = new StreamWriter(Path.Combine(_dir, "NGUSave.txt")))
            //{
            //    writer.WriteLine(data);
            //}

            //data = JsonUtility.ToJson(Character.importExport.gameStateToData());
            //using (var writer = new StreamWriter(Path.Combine(_dir, "NGUSave.json")))
            //{
            //    writer.WriteLine(data);
            //}

            //Character.saveLoad.saveGamestateToSteamCloud();
        }

        private void QuickLoad()
        {
            var filename = Path.Combine(_dir, "NGUSave.txt");
            if (!File.Exists(filename))
            {
                Log("Quicksave doesn't exist");
                return;
            }

            var saveTime = File.GetLastWriteTime(filename);
            var s = DateTime.Now.Subtract(saveTime);
            var secDiff = (int)s.TotalSeconds;
            if (secDiff > 120)
            {
                //var diff = saveTime.GetPrettyDate();

                //var confirmResult = MessageBox.Show($"Last quicksave was {diff}. Are you sure you want to load?",
                //    "Load Quicksave"
                //    , MessageBoxButtons.YesNo);

                //if (confirmResult == DialogResult.No)
                //    return;
            }

            Log("Loading quicksave");
            string base64Data;
            try
            {
                base64Data = File.ReadAllText(filename);
            }
            catch (Exception e)
            {
                Log($"Failed to read quicksave: {e.Message}");
                return;
            }

            try
            {
                //var saveDataFromString = Character.importExport.getSaveDataFromString(base64Data);
                //var dataFromString = Character.importExport.getDataFromString(base64Data);

                //if ((dataFromString == null || dataFromString.version < 361) &&
                //    Application.platform != RuntimePlatform.WindowsEditor)
                //{
                //    Log("Bad save version");
                //    return;
                //}

                //if (dataFromString.version > Character.getVersion())
                //{
                //    Log("Bad save version");
                //    return;
                //}

                //Character.saveLoad.loadintoGame(saveDataFromString);
            }
            catch (Exception e)
            {
                Log($"Failed to load quicksave: {e.Message}");
            }
        }

        // Stuff on a very short timer
        void QuickStuff()
        {
            if (!Settings.GlobalEnabled)
                return;
        }

        String buildingName(BuildingType id)
        {
            return Enum.GetName(typeof(BuildingType), id);
        }

        // Runs every MAIN_DELAY seconds, our main loop
        void AutomationRoutine()
        {
            try
            {
                if (!Settings.GlobalEnabled)
                    return;
                
                var player = FindObjectOfType<Player>();
                ManageFactories(player);
                ManageWorkOrders(player);
                ManageSpin(player);
            }
            catch (Exception e)
            {
                Log(e.Message);
                Log(e.StackTrace);
            }
            finally
            {
                _timeLeft = MAIN_DELAY;
            }
        }

        private void ManageSpin(Player player)
        {
            if (!Settings.AutoSpin)
                return;

            if (player.dailySpinController.getCurSpinCount() > 0)
            {
                Log($"SPINNING!! Spin count {player.dailySpinController.getCurSpinCount()} time: {player.spin.bankedSpinTime}");
                player.dailySpinController.Startspin();
                StartCoroutine(waiter());
            }

            System.Collections.IEnumerator waiter()
            {
                yield return new WaitForSeconds(20);
                var prize = player.dailySpinController.curPrizeText.text;
                Log($"SPINNING!! Spin prize {prize}");
                LogPitSpin($"Spin prize {prize}");
            }
        }

        private void ManagePit(Player player)
        {
            var controller = player.pitController;
            var pit = player.pit;
            foreach (var buildingType in Enum.GetValues(typeof(BuildingType)).Cast<BuildingType>())
            {
                int building = (int)buildingType;
                var yeeted = pit.items[building].amountYeeted;

            }
        }

        private void ManageWorkOrders(Player player)
        {
            var wo = player.workOrdersController;
            var buildings = wo.getValidBuildingsList();
            if (wo.getWorkOrderButton.interactable)
            {
                Log($"Getting new order");
                wo.newWorkOrderButtonClick();
            }
            Log($"Handing out all Work Order materials {buildings.Count()}");
            
            buildings.ForEach(building => wo.tryHandInMaterial(building));
        }

        private void ManageFactories(Player player)
        {
            Log($"On map: {player.factoryController.mapNameText.text}");
            var tracker = player.factoryController.tracker;

            Dictionary<BuildingType, double> toSet = new Dictionary<BuildingType, double>();
            Dictionary<BuildingType, double> toDelete = new Dictionary<BuildingType, double>();

            foreach (var buildingType in Enum.GetValues(typeof(BuildingType)).Cast<BuildingType>())
            {
                int building = (int)buildingType;
                var material = player.materials.materials[building];
                var properties = tracker.buildingProperties.properties[building];
                var gain = tracker.theoreticalGainLoss[building];
                var number = tracker.buildingCounts[building];
                var name = properties.name;
                Log($"Building?: {building}: {name} have: {material.amount} gain? {gain}");
                if (material.largestProduction > 0)
                {
                    var perSecond = material.largestProduction / properties.baseTime;
                    var shouldHave = perSecond * 3600 * 10;
                    if (buildingName(buildingType).Contains("Juice"))
                    {
                        shouldHave *= 100;
                    }
                    var havePercent = material.amount / shouldHave * 100;
                    var score = 0;// player.workOrdersController.getBuildingBaseScore(buildingType);
                    Log($"Output {material.largestProduction} time {properties.baseTime} per second {perSecond} have % {havePercent} score? {score}");

                    if (havePercent < 10 && gain < 0)
                    {
                        var extra = gain / perSecond / Math.Max(1, havePercent) * 100;
                        Log($"Adding to SET < 10% and gain < 0 extra {extra}");
                        toSet.Add(buildingType, havePercent + extra);
                    }
                    else if (havePercent < 10 && number < 3)
                    {
                        Log($"Adding to SET < 10% and less than 3 instances");
                        toSet.Add(buildingType, havePercent);
                    }
                    else if (havePercent < 1)
                    {
                        Log($"Adding to SET < 1%");
                        toSet.Add(buildingType, havePercent);
                    }
                    else if (havePercent < 100 && gain <= 0)
                    {
                        Log($"Adding to SET < 100% and gain <= 0");
                        toSet.Add(buildingType, havePercent);
                    }

                    if (havePercent > 1 && gain > 0 && number > 0)
                    {
                        Log($"Adding to DELETE");
                        toDelete.Add(buildingType, havePercent);
                    }
                }
            }
            Log($"To SET {string.Join(", ", toSet)}");
            Log($"To DELETE {string.Join(", ", toDelete)}");

            Dictionary<BuildingType, double> allDelete = Enum.GetValues(typeof(BuildingType)).Cast<BuildingType>().ToList()
                .FindAll(b => buildingName(b).Contains("Juice") || toDelete.ContainsKey(b))
                .ToDictionary(b => b, b => 0.0);

            var sorted = from entry in toSet orderby entry.Value select entry;
            foreach (var newBuilding in sorted)
            {
                var value = Math.Max(newBuilding.Value, -5000);
                while (value < 100)
                {
                    Log($"Value {value}");
                    SetOne(player, value <= 0 ? toDelete : toDelete.Where(b => b.Value > 200).ToDictionary(i => i.Key, i => i.Value), newBuilding.Key);
                    value += 100;
                }
            }
        }

        private static void SetOne(Player player, Dictionary<BuildingType, double> toDelete, BuildingType newBuilding)
        {
            var origMap = player.factoryController.curMapID;
            Log($"Setting {newBuilding}");
            var currentMap = 0;
            var maxEfficency = 0.0;
            var bestMap = 0;
            var bestIndex = -1;
            foreach (var map in player.factoryData.maps)
            {
                //Log($"Map unlocked?: {map.unlocked}");
                if (map.unlocked)
                {
                    player.factoryController.setNewMapID(currentMap);

                    var empty = player.factoryController.getAllEmptyTileIndexes(currentMap);
                    
                    empty.ForEach(index => IsBetter(BuildingType.None, index));
                   
                    foreach (var building in map.buildings)
                    {
                        var buildingType = building.building;
                        
                        if (toDelete.ContainsKey(buildingType))
                        {
                            IsBetter(buildingType, building.index);
                        }
                    }
                }
                currentMap++;
            }
            if (bestIndex != -1)
            {
                player.factoryController.setNewMapID(bestMap);
                Log($"DELETE {bestIndex} and setting {newBuilding} value {maxEfficency}");
                player.factoryController.doSetTile(bestIndex, newBuilding, TileDirection.Up);
            }
            player.factoryController.setNewMapID(origMap);

            void IsBetter(BuildingType buildingType, int index)
            {
                var production = player.factoryController.totalTileProductionMultiplier(currentMap, index, newBuilding);
                var speed = player.factoryController.totalTileSpeed(currentMap, index, newBuilding);
                var efficency = production * speed;
                if (buildingType == BuildingType.None)
                    efficency *= 100;
                if (efficency > maxEfficency)
                {
                    Log($"Building?: {buildingType} {index} ===> {production}*{speed} = {efficency}");
                    maxEfficency = efficency;
                    bestMap = currentMap;
                    bestIndex = index;
                }
            }
        }

        public void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 40), $"Automation - {(Settings.GlobalEnabled ? "Active" : "Inactive")}");
            GUI.Label(new Rect(10, 20, 200, 40), $"Next Loop - {_timeLeft:00.0}s");
        }

        public void MonitorLog()
        {
            //var bLog = Character.adventureController.log;
            //var type = bLog.GetType().GetField("Eventlog",
            //    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            //var val = type?.GetValue(bLog);
            //if (val == null)
            //    return;

            //var log = (List<string>) val;
            //for (var i = log.Count - 1; i >= 0; i--)
            //{
            //    var line = log[i];
            //    if (!line.Contains("dropped")) continue;
            //    if (line.Contains("gold")) continue;
            //    if (line.ToLower().Contains("special boost")) continue;
            //    if (line.ToLower().Contains("toughness boost")) continue;
            //    if (line.ToLower().Contains("power boost")) continue;
            //    if (line.Contains("EXP")) continue;
            //    if (line.EndsWith("<b></b>")) continue;
            //    var result = line;
            //    if (result.Contains("\n"))
            //    {
            //        result = result.Split('\n').Last();
            //    }

            //    var sb = new StringBuilder(result);
            //    sb.Replace("<color=blue>", "");
            //    sb.Replace("<b>", "");
            //    sb.Replace("</color>", "");
            //    sb.Replace("</b>", "");

            //    LogLoot(sb.ToString());
            //    log[i] = $"{line}<b></b>";
            //}
        }


        public void OnApplicationQuit()
        {
            Loader.Unload();
        }
    }
}
