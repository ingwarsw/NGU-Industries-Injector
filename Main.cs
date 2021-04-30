using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using UnityEngine;

namespace NGUIndustriesInjector
{
    internal class Main : MonoBehaviour
    {
        private const float MAIN_DELAY = 60;

        internal static StreamWriter OutputWriter;
        internal static StreamWriter LootWriter;
        internal static StreamWriter CombatWriter;
        internal static StreamWriter PitSpinWriter;
        internal static Main Reference { get; set; }
        internal static bool IgnoreNextChange { get; set; }
        internal static SettingsForm SettingsForm { get; set; }
        internal static SavedSettings Settings { get; set; }

        private float _timeLeft = MAIN_DELAY;

        private string _dir { get; set; }

        private FileSystemWatcher ConfigWatcher { get; set; }

        private List<MaterialState> materialState;


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
                SettingsForm.Close();
                SettingsForm.Dispose();

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

                var _profilesDir = Path.Combine(_dir, "profiles");
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
                SetUpDDLLoading();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Loader intialization error: {e.Message}");
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
                SettingsForm = new SettingsForm();

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
                    SettingsForm.UpdateFromSettings(Settings);
                };

                Settings.LoadSettings();
                SettingsForm.UpdateFromSettings(Settings);
                SettingsForm.Show();

                InvokeRepeating("AutomationRoutine", 0.0f, MAIN_DELAY);
                InvokeRepeating("SnipeZone", 0.0f, .1f);
                InvokeRepeating("MonitorLog", 0.0f, 1f);
                InvokeRepeating("QuickStuff", 0.0f, .5f);
                InvokeRepeating("ShowBoostProgress", 0.0f, 60.0f);
                InvokeRepeating("SetResnipe", 0f, 1f);

                Reference = this;

                materialState = new List<MaterialState>();
                MaterialState.SetState(materialState, FindObjectOfType<Player>());
                foreach (var buildingType in Enum.GetValues(typeof(BuildingType)).Cast<BuildingType>())
                {
                    materialState.Add(new MaterialState(buildingType));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Loader startup error: {e.Message}");
                Log(e.ToString());
                Log(e.StackTrace);
                Log(e.InnerException.ToString());
            }
        }

        private void SetUpDDLLoading()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
        }

        internal static void UpdateForm(SavedSettings newSettings)
        {
            SettingsForm.UpdateFromSettings(newSettings);
        }

        public void Update()
        {
            _timeLeft -= Time.deltaTime;

            SettingsForm.UpdateProgressBar((int)Math.Floor(_timeLeft / MAIN_DELAY * 100));

            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (!SettingsForm.Visible)
                {

                    SettingsForm.Show();
                }
                SettingsForm.BringToFront();
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
                ManageCombat(player);
                ManageFarms(player);
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

        private void ManageFarms(Player player)
        {
            if (Settings.ManageFarms && player.farm.unlocked)
            {
                var index = 0;
                player.farm.plots.ForEach(plot => {
                    Log($"Farm plot {index}: {plot.plotTimer} max? { player.farmController.maxGrowTime()}");
                    if (plot.plotTimer >= player.farmController.maxGrowTime())
                    {
                        Log($"Harvesting {index} plant: {plot.plantIndex}");
                        player.farmController.trySelectNewPlant(plot.plantIndex);
                        player.farmController.tryHarvestPlot(index);
                        player.farmController.tryStartPlanting(index);
                    }
                    index++;
                });

                index = 0;
                player.farm.breeds.ForEach(breed =>
                {
                    Log($"Farm breed {index}: {breed.breedTime} max? {player.farmController.maxBreedTime()}");
                    if (breed.breedTime >= player.farmController.maxBreedTime())
                    {
                        var plantIndex1 = breed.plantIndex1;
                        var plantIndex2 = breed.plantIndex2;
                        Log($"Harvesting {index} breed: {plantIndex1}:{plantIndex2}");
                        player.farmController.tryEndBreeding(index);
                        //player.farmController.trySelectNewPlant(plantIndex1);
                        player.farmController.setLeftBreed(plantIndex1);
                        player.farmController.setRightBreed(plantIndex2);
                        player.farmController.tryStartBreeding(index);
                    }
                    index++;
                });
               
            }
        }

        private void ManageCombat(Player player)
        {
            var offenseRating = player.combatController.playerOffenseRating();
            var defenseRating = player.combatController.playerDefenseRating();
            var level = player.combatController.getIsopodLevelFromRating(offenseRating + defenseRating);
            var currentLevel = player.combat.selectedFloor;
            if (level != currentLevel)
            {
                //player.combatController.setNewIsopodLevel(level);
            }

            Log($"Combat: rating {offenseRating}({player.combatController.playerOffense})/{defenseRating} level {level} current {currentLevel}");
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
            if (Settings.ManageWorkOrders)
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
        }

        private void ManageFactories(Player player)
        {
            Log($"On map: {player.factoryController.mapNameText.text}");
            var tracker = player.factoryController.tracker;

            materialState.ForEach(state => state.CalculateState());

            var sorted = from entry in materialState where entry.BuildNumber > 0 orderby entry.BuildPercent select entry;
            foreach (var newBuilding in sorted)
            {
                for (int i = 0; i < newBuilding.BuildNumber; i++)
                {
                    SetOne(player, newBuilding);
                }
            }
        }

        private static void SetOne(Player player, MaterialState newBuilding)
        {
            var origMap = player.factoryController.curMapID;
            var toDelete = newBuilding.ToDelete;
            Log($"Setting {newBuilding} to delete {string.Join(", ", toDelete)}");
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

                        if (toDelete.Contains(buildingType))
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
                player.factoryController.doSetTile(bestIndex, newBuilding.BuildingType, TileDirection.Up);
            }
            player.factoryController.setNewMapID(origMap);

            void IsBetter(BuildingType buildingType, int index)
            {
                var production = player.factoryController.totalTileProductionMultiplier(currentMap, index, newBuilding.BuildingType);
                var testSpeed = player.factoryController.totalTileSpeed(currentMap, index, BuildingType.CandyJuice4);
                var speed = player.factoryController.totalTileSpeed(currentMap, index, newBuilding.BuildingType);
                var efficency = production * speed;
                var realTime = newBuilding.BaseTime / speed;

                // we preffer empty places
                if (buildingType == BuildingType.None)
                    efficency *= 100;

                // We are not properly using speed beacon, lets penalize it
                if (testSpeed > 1 && realTime == newBuilding.MinTime)
                    efficency /= 100;

                if (efficency > maxEfficency)
                {
                    Log($"Building?: {buildingType} {index} ===> {production}*{speed} (real time: {realTime} == min time: {newBuilding.MinTime})= {efficency} (test speed {testSpeed})");
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
