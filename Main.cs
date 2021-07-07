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
        internal static StreamWriter DebugWriter;
        internal static StreamWriter LootWriter;
        internal static StreamWriter CombatWriter;
        internal static StreamWriter PitSpinWriter;

        internal static Main Reference { get; set; }
        internal static bool IgnoreNextChange { get; set; }
        internal static SettingsForm SettingsForm { get; private set; }
        internal static SavedSettings Settings { get; set; }

        private float _timeLeft = MAIN_DELAY;

        private string _dir { get; set; }

        private FileSystemWatcher ConfigWatcher { get; set; }

        private List<MaterialState> materialState;

        internal static void Debug(string msg)
        {
#if DEBUG
            DebugWriter.WriteLine($"{ DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()}: {msg}");
#endif
        }

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
                CancelInvoke("QuickStuff");

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
                DebugWriter = new StreamWriter(Path.Combine(logDir, "debug.log")) { AutoFlush = true };
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

                InvokeRepeating("AutomationRoutine", 1.0f, MAIN_DELAY);
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
                ManagePit(player);
                ManageExperiments(player);
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

        private void ManageExperiments(Player player)
        {
            if (!Settings.ManageExperiments || !player.experiments.unlocked)
            {
                return;
            }

            var experimentsController = player.experimentsController;
            var queueIndexToRewardFactor = ExperimentUtilities.GetQueueIndexToRewardFactor(Settings.WeightedRewards, player.experiments);
            var orderedByRewards = queueIndexToRewardFactor.OrderByDescending(t => t.Value);

            var queuedExperiments = player.experiments.queuedExperiments;
            var activeCount = experimentsController.getActiveCount();
            var maxActiveCount = experimentsController.maxActiveExperiments();

            var frozenExperiments = queuedExperiments
                .Where(exp => exp.isFrozen)
                .OrderByDescending(exp => queueIndexToRewardFactor[queuedExperiments.IndexOf(exp)]).ToList();

            if (Settings.FreezeExperiments)
            {
                var maxFrozenExperiments = experimentsController.maxFrozenExperiments();
                for (int i = 0; i < maxFrozenExperiments; i++)
                {
                    KeyValuePair<int, double> optimalExperiment;
                    if (Settings.WeightedRewards)
                    {
                        var orderedWeightedRewards = ExperimentUtilities.GetQueueIndexToRewardFactor(
                                                        true,
                                                        player.experiments,
                                                        frozenExperiments.Take(i))
                                                    .OrderByDescending(t => t.Value);

                        optimalExperiment = orderedWeightedRewards.First();
                    }
                    else
                    {
                        optimalExperiment = orderedByRewards.Skip(i).First();
                    }

                    if (frozenExperiments.Any(frozenExperiment => queuedExperiments.IndexOf(frozenExperiment) == optimalExperiment.Key))
                    {
                        Debug($"Did not attempt to freeze Experiment {optimalExperiment.Key} since it's already frozen");
                    }
                    else
                    {
                        var frozen = frozenExperiments.Skip(i).FirstOrDefault();
                        if (frozen == null)
                        {
                            if (queuedExperiments[optimalExperiment.Key].isFrozen)
                            {
                                Debug($"Prevented already frozen Experiment {optimalExperiment.Key} from being unfrozen");
                            }
                            else
                            {
                                Debug($"Attempted to freeze Experiment {optimalExperiment.Key}, Factor: {optimalExperiment.Value}");
                                experimentsController.tryToggleFreezeExperiment(optimalExperiment.Key);

                                frozenExperiments.Insert(0, queuedExperiments[optimalExperiment.Key]);
                            }
                        }
                        else
                        {
                            var indexOfFrozen = queuedExperiments.IndexOf(frozen);
                            if (indexOfFrozen != optimalExperiment.Key)
                            {
                                experimentsController.tryToggleFreezeExperiment(indexOfFrozen);
                                Debug($"Attempted to Un-Freeze Experiment {indexOfFrozen} since its factor is lower than the higher factor of Experiment {optimalExperiment.Key}");
                                frozenExperiments.Remove(frozen);

                                experimentsController.tryToggleFreezeExperiment(optimalExperiment.Key);
                                Debug($"Attempted to Freeze Experiment {optimalExperiment.Key}, Factor: {optimalExperiment.Value}");
                                frozenExperiments.Insert(0, queuedExperiments[optimalExperiment.Key]);
                            }
                            else
                            {
                                Debug($"Did not attempt to freeze Experiment {indexOfFrozen} since we're matching the highest factor index of {optimalExperiment.Key} already");
                            }
                        }
                    }
                }

                while (activeCount < maxActiveCount)
                {
                    if (frozenExperiments.Any())
                    {
                        var frozenExperiment = frozenExperiments.FirstOrDefault();
                        var index = queuedExperiments.IndexOf(frozenExperiment);
                        experimentsController.tryStartExperiment(index);
                        activeCount++;
                        frozenExperiments.Remove(frozenExperiment);
                    }
                    else
                    {
                        Debug("Encountered invalid state. Attempting to Start the first frozen experiment failed because there are none.");
                    }
                }
            }
            else while (activeCount < maxActiveCount)
            {
                if (frozenExperiments.Any())
                {
                    var frozen = frozenExperiments.First();
                    var indexOfFrozen = queuedExperiments.IndexOf(frozen);
                    experimentsController.tryStartExperiment(indexOfFrozen);
                    activeCount++;
                    frozenExperiments.Remove(frozen);
                }
                else
                {
                    int activeIndex = activeCount;
                    var activeExperiments = new List<Experiment>();
                    while (activeCount < maxActiveCount)
                    {
                        var index = orderedByRewards.Skip(activeIndex++).First().Key;
                        if (Settings.WeightedRewards)
                        {
                                var orderedWeightedRewards = ExperimentUtilities.GetQueueIndexToRewardFactor(
                                                               true,
                                                               player.experiments,
                                                               activeExperiments)
                                                            .OrderByDescending(t => t.Value);

                                index = orderedWeightedRewards.First().Key;
                        }

                        experimentsController.tryStartExperiment(index);
                        activeExperiments.Add(queuedExperiments.ElementAt(index));
                        activeCount++;
                    }
                }
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
            //var level = player.combatController.getIsopodLevelFromRating(offenseRating + defenseRating);
            var level = (int)(Math.Log(defenseRating / 20, player.combatController.ISOPOD_LEVEL_SCALE()));
            var currentLevel = player.combat.selectedFloor;
            Log($"Combat: rating {offenseRating}({player.combatController.playerOffense})/{defenseRating} level {level} current {currentLevel}");
            if (level != currentLevel)
            {
                Log($"Combat: Setting new level {level} (old {currentLevel})");
                player.combatController.setNewIsopodLevel(level);
                player.combatController.updateMenu();
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
            if (Settings.ManagePit)
            {
                var controller = player.pitController;
                var pit = player.pit;
                foreach (var state in materialState)
                {
                    var yeeted = pit.items[state.BuildingId].amountYeeted;
                    Log($"PIT before: Will yeet {state} yeeted already {yeeted} (hard cap {controller.HARDCAP_YEETED()})");
                    if (yeeted < state.PerSecond * Settings.PitThreshold && state.Amount > 0 && state.Gain > 0 && !player.invalidBuildingID(state.BuildingType))
                    {
                        Log($"PIT: Will yeet {state} yeeted already {yeeted}");
                        controller.submitItems(state.BuildingType);
                        player.confirmationBox.closeBox();
                        controller.actuallySubmitItems();
                        Log($"PIT: Yeeted {materialState[state.BuildingId]} yeeted already {pit.items[state.BuildingId].amountYeeted}");
                        return;
                    }
                }
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
            if (Settings.ManageFactories)
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
            GUI.VerticalSlider(new Rect(10, 50, 20, 400), (int)Math.Floor(_timeLeft / MAIN_DELAY * 100), 100, 0);
        }

        public void OnApplicationQuit()
        {
            Loader.Unload();
        }
    }
}
