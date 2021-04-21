using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace NGUInjector
{
    internal class Main : MonoBehaviour
    {
        internal static StreamWriter OutputWriter;
        internal static StreamWriter LootWriter;
        internal static StreamWriter CombatWriter;
        internal static StreamWriter AllocationWriter;
        internal static StreamWriter PitSpinWriter;
        internal static Main reference;
        private float _timeLeft = 60.0f;
        internal static SettingsForm settingsForm;
        internal const string Version = "3.1.0";

        private static string _dir;
        private static string _profilesDir;

        private static bool _tempSwapped = false;

        internal static FileSystemWatcher ConfigWatcher;
        internal static FileSystemWatcher AllocationWatcher;
        internal static FileSystemWatcher ZoneWatcher;

        internal static bool IgnoreNextChange { get; set; }

        internal static SavedSettings Settings;

        internal static void Log(string msg)
        {
            OutputWriter.WriteLine($"{ DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()} (s): {msg}");
        }

        internal static void LogLoot(string msg)
        {
            LootWriter.WriteLine($"{ DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()} (s): {msg}");
        }

        internal static void LogCombat(string msg)
        {
            //CombatWriter.WriteLine($"{DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()} ({Math.Floor(Character.rebirthTime.totalseconds)}s): {msg}");
        }

        internal static void LogPitSpin(string msg)
        {
            //PitSpinWriter.WriteLine($"{DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()} ({Math.Floor(Character.rebirthTime.totalseconds)}s): {msg}");
        }

        internal static void LogAllocation(string msg)
        {
            if (!Settings.DebugAllocation) return;
            //AllocationWriter.WriteLine($"{DateTime.Now.ToShortDateString()}-{ DateTime.Now.ToShortTimeString()} ({Math.Floor(Character.rebirthTime.totalseconds)}s): {msg}");
        }

        internal void Unload()
        {
            try
            {
                CancelInvoke("AutomationRoutine");
                CancelInvoke("SnipeZone");
                CancelInvoke("MonitorLog");
                CancelInvoke("QuickStuff");
                CancelInvoke("SetResnipe");
                CancelInvoke("ShowBoostProgress");


                LootWriter.Close();
                CombatWriter.Close();
                AllocationWriter.Close();
                PitSpinWriter.Close();
                settingsForm.Close();
                settingsForm.Dispose();

                ConfigWatcher.Dispose();
                AllocationWatcher.Dispose();
                ZoneWatcher.Dispose();
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
            OutputWriter.Close();
        }

        public void Start()
        {
            //System.Windows.Forms.MessageBox.Show("Main Start");
            try
            {
                _dir = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%/Desktop"), "NGUInjector");
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
                AllocationWriter = new StreamWriter(Path.Combine(logDir, "allocation.log")) { AutoFlush = true };
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
                //Character = FindObjectOfType<Character>();

                Log("Injected");
                LogLoot("Starting Loot Writer");
                LogCombat("Starting Combat Writer");
                //Controller = Character.inventoryController;
                //PlayerController = FindObjectOfType<PlayerController>();
                //_invManager = new InventoryManager();
                //_yggManager = new YggdrasilManager();
                //_questManager = new QuestManager();
                //_combManager = new CombatManager();
                //LoadoutManager.ReleaseLock();
                //DiggerManager.ReleaseLock();

                Settings = new SavedSettings(_dir);

                if (!Settings.LoadSettings())
                {
                    var temp = new SavedSettings(null)
                    {
                        PriorityBoosts = new int[] { },
                        YggdrasilLoadout = new int[] { },
                        SwapYggdrasilLoadouts = false,
                        HighestAKZone = 0,
                        SwapTitanLoadouts = false,
                        TitanLoadout = new int[] { },
                        ManageDiggers = true,
                        ManageYggdrasil = true,
                        ManageEnergy = true,
                        ManageMagic = true,
                        ManageInventory = true,
                        ManageGear = true,
                        AutoConvertBoosts = true,
                        SnipeZone = 0,
                        FastCombat = false,
                        PrecastBuffs = true,
                        AutoFight = false,
                        AutoQuest = false,
                        AutoQuestITOPOD = false,
                        AllowMajorQuests = false,
                        GoldDropLoadout = new int[] { },
                        AutoMoneyPit = false,
                        AutoSpin = false,
                        MoneyPitLoadout = new int[] { },
                        AutoRebirth = false,
                        ManageWandoos = false,
                        MoneyPitThreshold = 1e5,
                        DoGoldSwap = false,
                        BoostBlacklist = new int[] { },
                        CombatMode = 0,
                        RecoverHealth = false,
                        SnipeBossOnly = true,
                        AllowZoneFallback = false,
                        QuestFastCombat = true,
                        AbandonMinors = false,
                        MinorAbandonThreshold = 30,
                        QuestCombatMode = 0,
                        AutoBuyEM = false,
                        AutoSpellSwap = false,
                        CounterfeitThreshold = 400,
                        SpaghettiThreshold = 30,
                        BloodNumberThreshold = 1e10,
                        CastBloodSpells = false,
                        IronPillThreshold = 1e5,
                        BloodMacGuffinAThreshold = 6,
                        BloodMacGuffinBThreshold = 6,
                        CubePriority = 0,
                        CombatEnabled = false,
                        GlobalEnabled = false,
                        QuickDiggers = new int[] { },
                        QuickLoadout = new int[] { },
                        UseButterMajor = false,
                        ManualMinors = false,
                        UseButterMinor = false,
                        ActivateFruits = false,
                        ManageR3 = true,
                        WishPriorities = new int[] { },
                        BeastMode = true,
                        ManageNGUDiff = true,
                        AllocationFile = "default",
                        //TitanGoldTargets = new bool[ZoneHelpers.TitanZones.Length],
                        //ManageGoldLoadouts = false,
                        //ResnipeTime = 3600,
                        //TitanMoneyDone = new bool[ZoneHelpers.TitanZones.Length],
                        //TitanSwapTargets = new bool[ZoneHelpers.TitanZones.Length],
                        GoldCBlockMode = false,
                        DebugAllocation = false,
                        AdventureTargetITOPOD = false
                    };

                    Settings.MassUpdate(temp);

                    Log($"Created default settings");
                }

                settingsForm = new SettingsForm();

                if (string.IsNullOrEmpty(Settings.AllocationFile))
                {
                    Settings.SetSaveDisabled(true);
                    Settings.AllocationFile = "default";
                    Settings.SetSaveDisabled(false);
                }

                if (Settings.TitanGoldTargets == null || Settings.TitanGoldTargets.Length == 0)
                {
                    Settings.SetSaveDisabled(true);
                    //Settings.TitanGoldTargets = new bool[ZoneHelpers.TitanZones.Length];
                    //Settings.TitanMoneyDone = new bool[ZoneHelpers.TitanZones.Length];
                    Settings.SetSaveDisabled(false);
                }

                if (Settings.TitanMoneyDone == null || Settings.TitanMoneyDone.Length == 0)
                {
                    Settings.SetSaveDisabled(true);
                    //Settings.TitanGoldTargets = new bool[ZoneHelpers.TitanZones.Length];
                    //Settings.TitanMoneyDone = new bool[ZoneHelpers.TitanZones.Length];
                    Settings.SetSaveDisabled(false);
                }

                if (Settings.TitanSwapTargets == null || Settings.TitanSwapTargets.Length == 0)
                {
                    Settings.SetSaveDisabled(true);
                    //Settings.TitanSwapTargets = new bool[ZoneHelpers.TitanZones.Length];
                    Settings.SetSaveDisabled(false);
                }

                LoadAllocation();
                LoadAllocationProfiles();

                ZoneWatcher = new FileSystemWatcher
                {
                    Path = _dir,
                    Filter = "zoneOverride.json",
                    NotifyFilter = NotifyFilters.LastWrite,
                    EnableRaisingEvents = true
                };

                ZoneWatcher.Changed += (sender, args) =>
                {
                    Log(_dir);
                    //ZoneStatHelper.CreateOverrides(_dir);
                };

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
                    LoadAllocation();
                };

                AllocationWatcher = new FileSystemWatcher
                {
                    Path = _profilesDir,
                    Filter = "*.json",
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                    EnableRaisingEvents = true
                };

                AllocationWatcher.Changed += (sender, args) => { LoadAllocation(); };
                AllocationWatcher.Created += (sender, args) => { LoadAllocationProfiles(); };
                AllocationWatcher.Deleted += (sender, args) => { LoadAllocationProfiles(); };
                AllocationWatcher.Renamed += (sender, args) => { LoadAllocationProfiles(); };

                Settings.SaveSettings();
                Settings.LoadSettings();

                LogAllocation("Started Allocation Writer");

                //ZoneStatHelper.CreateOverrides(_dir);

                settingsForm.UpdateFromSettings(Settings);
                settingsForm.Show();

                InvokeRepeating("AutomationRoutine", 0.0f, 60.0f);
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

            settingsForm.UpdateProgressBar((int)Math.Floor(_timeLeft / 10 * 100));

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

            if (Input.GetKeyDown(KeyCode.F4))
            {
                Settings.AutoQuestITOPOD = !Settings.AutoQuestITOPOD;
            }

            if (Input.GetKeyDown(KeyCode.F5))
            {
                DumpEquipped();
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                if (Settings.QuickLoadout.Length > 0)
                {
                    if (_tempSwapped)
                    {
                        Log("Restoring Previous Loadout");
                        //LoadoutManager.RestoreTempLoadout();
                    }
                    else
                    {
                        Log("Equipping Quick Loadout");
                        //LoadoutManager.SaveTempLoadout();
                        //LoadoutManager.ChangeGear(Settings.QuickLoadout);
                    }
                }

                if (Settings.QuickDiggers.Length > 0)
                {
                    if (_tempSwapped)
                    {
                        Log("Equipping Previous Diggers");
                        //DiggerManager.RestoreTempDiggers();
                    }
                    else
                    {
                        Log("Equipping Quick Diggers");
                        //DiggerManager.SaveTempDiggers();
                        //DiggerManager.EquipDiggers(Settings.QuickDiggers);
                    }
                }

                _tempSwapped = !_tempSwapped;
            }

            // F11 reserved for testing
            //if (Input.GetKeyDown(KeyCode.F11))
            //{
            //    Character.realExp += 10000;
            //}
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

            //Turn on autoattack if we're in ITOPOD and its not on
            //if (Settings.AutoQuestITOPOD && Character.adventureController.zone >= 1000 && !Character.adventure.autoattacking && !Settings.CombatEnabled)
            //{
            //    //Character.adventureController.idleAttackMove.setToggle();
            //}

            if (Settings.AutoFight)
            {
                //var needsAllocation = false;
                //var bc = Character.bossController;
                //if (!bc.isFighting && !bc.nukeBoss)
                //{
                //    if (Character.bossID == 0)
                //        needsAllocation = true;

                //    if (bc.character.attack / 5.0 > bc.character.bossDefense && bc.character.defense / 5.0 > bc.character.bossAttack)
                //        bc.startNuke();
                //    else
                //    {
                //        if (bc.character.attack > (bc.character.bossDefense * 1.4) && bc.character.defense > bc.character.bossAttack * 1.4)
                //        {
                //            bc.beginFight();
                //            bc.stopButton.gameObject.SetActive(true);
                //        }
                //    }
                //}

                //if (needsAllocation)
                //{
                //    //_profile.DoAllocations();
                //}
            }

            if (Settings.AutoMoneyPit)
            {
                //MoneyPitManager.CheckMoneyPit();
            }

            if (Settings.AutoSpin)
            {
                //MoneyPitManager.DoDailySpin();
            }

            if (Settings.AutoQuestITOPOD)
            {
                MoveToITOPOD();
            }

            if (Settings.AutoSpellSwap)
            {
                //var spaghetti = (Character.bloodMagicController.lootBonus() - 1) * 100;
                //var counterfeit = ((Character.bloodMagicController.goldBonus() - 1)) * 100;
                //var number = Character.bloodMagic.rebirthPower;
                //Character.bloodMagic.rebirthAutoSpell = Settings.BloodNumberThreshold > 0 && Settings.BloodNumberThreshold >= number;
                //Character.bloodMagic.goldAutoSpell = Settings.CounterfeitThreshold > 0 && Settings.CounterfeitThreshold >= counterfeit;
                //Character.bloodMagic.lootAutoSpell = Settings.SpaghettiThreshold > 0 && Settings.SpaghettiThreshold >= spaghetti;
                //Character.bloodSpells.updateGoldToggleState();
                //Character.bloodSpells.updateLootToggleState();
                //Character.bloodSpells.updateRebirthToggleState();
            }
        }

        String buildingName(BuildingType id)
        {
            return Enum.GetName(typeof(BuildingType), id);
        }

        // Runs every 10 seconds, our main loop
        void AutomationRoutine()
        {
            try
            {
                var player = FindObjectOfType<Player>();
                ManageFactories(player);
                ManageWorkOrders(player);



                //    if (!Settings.GlobalEnabled)
                //    {
                //        _timeLeft = 10f;
                //        return;
                //    }

                //    //ZoneHelpers.OptimizeITOPOD();

                //    if (Settings.ManageInventory && !Controller.midDrag)
                //    {
                //        //var converted = Character.inventory.GetConvertedInventory().ToArray();
                //        //var boostSlots = _invManager.GetBoostSlots(converted);
                //        //_invManager.EnsureFiltered(converted);
                //        //_invManager.ManageConvertibles(converted);
                //        //_invManager.MergeEquipped(converted);
                //        //_invManager.MergeInventory(converted);
                //        //_invManager.MergeBoosts(converted);
                //        //_invManager.MergeGuffs(converted);
                //        //_invManager.BoostInventory(boostSlots);
                //        //_invManager.BoostInfinityCube();
                //        //_invManager.ManageBoostConversion(boostSlots);
                //    }

                //    //if (Settings.ManageInventory && !Controller.midDrag)
                //    //{
                //    //    var watch = Stopwatch.StartNew();
                //    //    var converted = Character.inventory.GetConvertedInventory().ToArray();
                //    //    Log($"Creating CI: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    var boostSlots = _invManager.GetBoostSlots(converted);
                //    //    Log($"Get Boost Slots: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    _invManager.EnsureFiltered(converted);
                //    //    Log($"Filtering: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    _invManager.ManageConvertibles(converted);
                //    //    Log($"Convertibles: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    _invManager.MergeEquipped(converted);
                //    //    Log($"Merge Equipped: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    _invManager.MergeInventory(converted);
                //    //    Log($"Merge Inventory: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    _invManager.MergeBoosts(converted);
                //    //    Log($"Merge Boosts: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    _invManager.MergeGuffs(converted);
                //    //    Log($"Merge Guffs: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    _invManager.BoostInventory(boostSlots);
                //    //    Log($"Boost Inventory: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    _invManager.BoostInfinityCube();
                //    //    Log($"Boost Cube: {watch.ElapsedMilliseconds}");
                //    //    watch = Stopwatch.StartNew();
                //    //    _invManager.ManageBoostConversion(boostSlots);
                //    //    Log($"Boost Conversion: {watch.ElapsedMilliseconds}");
                //    //    watch.Stop();
                //    //}

                //    if (Settings.SwapTitanLoadouts)
                //    {
                //        //LoadoutManager.TryTitanSwap();
                //        //DiggerManager.TryTitanSwap();
                //    }

                //    //if (Settings.ManageYggdrasil && Character.buttons.yggdrasil.interactable)
                //    //{
                //    //    //_yggManager.ManageYggHarvest();
                //    //    //_yggManager.CheckFruits();
                //    //}

                //    if (Settings.AutoBuyEM)
                //    {
                //        //We haven't unlocked custom purchases yet
                //        if (Character.highestBoss < 17) return;

                //        var ePurchase = Character.energyPurchases;
                //        var mPurchase = Character.magicPurchases;
                //        var r3Purchase = Character.res3Purchases;

                //        var energy = ePurchase.customAllCost() > 0;
                //        var r3 = Character.res3.res3On && r3Purchase.customAllCost() > 0;
                //        var magic = Character.highestBoss >= 37 && mPurchase.customAllCost() > 0;

                //        long total = 0;

                //        if (energy)
                //        {
                //            total += ePurchase.customAllCost();
                //        }

                //        if (magic)
                //        {
                //            total += mPurchase.customAllCost();
                //        }

                //        if (r3)
                //        {
                //            total += r3Purchase.customAllCost();
                //        }

                //        var numPurchases = Math.Floor((double)(Character.realExp / total));

                //        if (numPurchases > 0)
                //        {
                //            var t = string.Empty;
                //            if (energy)
                //            {
                //                t += "/exp";
                //            }

                //            if (magic)
                //            {
                //                t += "/magic";
                //            }

                //            if (r3)
                //            {
                //                t += "/res3";
                //            }

                //            t = t.Substring(1);

                //            Log($"Buying {numPurchases} {t} purchases");
                //            for (var i = 0; i < numPurchases; i++)
                //            {
                //                if (energy)
                //                {
                //                    var ePurchaseMethod = ePurchase.GetType().GetMethod("buyCustomAll",
                //                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                //                    ePurchaseMethod?.Invoke(ePurchase, null);
                //                }

                //                if (magic)
                //                {
                //                    var mPurchaseMethod = mPurchase.GetType().GetMethod("buyCustomAll",
                //                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                //                    mPurchaseMethod?.Invoke(mPurchase, null);
                //                }

                //                if (r3)
                //                {
                //                    var r3PurchaseMethod = r3Purchase.GetType().GetMethod("buyCustomAll",
                //                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                //                    r3PurchaseMethod?.Invoke(r3Purchase, null);
                //                }
                //            }
                //        }
                //    }

                //    //_profile.DoAllocations();

                //    if (Settings.AutoQuest && Character.buttons.beast.interactable)
                //    {
                //        //var converted = Character.inventory.GetConvertedInventory().ToArray();
                //        //if (!Character.inventoryController.midDrag)
                //        //    _invManager.ManageQuestItems(converted);
                //        //_questManager.CheckQuestTurnin();
                //        //_questManager.ManageQuests();
                //    }

                //    if (Settings.AutoRebirth)
                //    {
                //        if (!Character.bossController.isFighting && !Character.bossController.nukeBoss)
                //        {
                //            //_profile.DoRebirth();
                //        }
                //        else
                //        {
                //            Log("Delaying rebirth while boss fight is in progress");
                //        }
                //    }
            }
            catch (Exception e)
            {
                Log(e.Message);
                Log(e.StackTrace);
            }
            _timeLeft = 60f;
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
            if (buildings.Count() > 0)
            {
                Log($"Getting new order");
                wo.newWorkOrderButtonClick();
            }
            Log($"Handing out all Work Order materials");
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
                    Log($"Output {material.largestProduction} time {properties.baseTime} per second {perSecond} have % {havePercent}");

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

        internal static void LoadAllocation()
        {
            //_profile = new CustomAllocation(_profilesDir, Settings.AllocationFile);
            //_profile.ReloadAllocation();
        }

        private static void LoadAllocationProfiles()
        {
            var files = Directory.GetFiles(_profilesDir);
            settingsForm.UpdateProfileList(files.Select(Path.GetFileNameWithoutExtension).ToArray(), Settings.AllocationFile);
        }

        private void SnipeZone()
        {
            if (!Settings.GlobalEnabled)
                return;

            ////If tm ever drops to 0, reset our gold loadout stuff
            //if (Character.machine.realBaseGold == 0.0 && !Settings.DoGoldSwap)
            //{
            //    Log("Time Machine Gold is 0. Lets reset gold snipe zone.");
            //    Settings.DoGoldSwap = true;
            //    Settings.TitanMoneyDone = new bool[ZoneHelpers.TitanZones.Length];
            //}

            ////This logic should trigger only if Time Machine is ready
            //if (Character.buttons.brokenTimeMachine.interactable)
            //{
            //    if (Character.machine.realBaseGold == 0.0)
            //    {
            //        _combManager.ManualZone(0, false, false, false, true, false);
            //        return;
            //    }
            //    //Go to our gold loadout zone next to get a high gold drop
            //    if (Settings.ManageGoldLoadouts && Settings.DoGoldSwap)
            //    {
            //        if (LoadoutManager.TryGoldDropSwap())
            //        {
            //            var bestZone = ZoneStatHelper.GetBestZone();
            //            _furthestZone = ZoneHelpers.GetMaxReachableZone(false);

            //            _combManager.ManualZone(bestZone.Zone, true, bestZone.FightType == 1, false, bestZone.FightType == 2, false);
            //            return;
            //        }
            //    }
            //}

            //var questZone = _questManager.IsQuesting();
            //if (questZone > 0)
            //{
            //    if (Settings.QuestCombatMode == 0)
            //    {
            //        _combManager.ManualZone(questZone, false, false, false, Settings.QuestFastCombat, Settings.BeastMode);
            //    }
            //    else
            //    {
            //        _combManager.IdleZone(questZone, false, false);
            //    }

            //    return;
            //}

            //if (!Settings.CombatEnabled)
            //    return;

            //if (Settings.SnipeZone < 0)
            //    return;

            //var tempZone = Settings.AdventureTargetITOPOD ? 1000 : Settings.SnipeZone;
            //if (tempZone < 1000)
            //{
            //    if (!CombatManager.IsZoneUnlocked(Settings.SnipeZone))
            //    {
            //        tempZone = Settings.AllowZoneFallback ? ZoneHelpers.GetMaxReachableZone(false) : 1000;
            //    }
            //    else
            //    {
            //        if (ZoneHelpers.ZoneIsTitan(Settings.SnipeZone) && !ZoneHelpers.TitanSpawningSoon(Array.IndexOf(ZoneHelpers.TitanZones, Settings.SnipeZone)))
            //        {
            //            tempZone = 1000;
            //        }
            //    }
            //}

            //if (Settings.CombatMode == 0)
            //{
            //    _combManager.ManualZone(tempZone, Settings.SnipeBossOnly, Settings.RecoverHealth, Settings.PrecastBuffs, Settings.FastCombat, Settings.BeastMode);
            //}
            //else
            //{
            //    _combManager.IdleZone(tempZone, Settings.SnipeBossOnly, Settings.RecoverHealth);
            //}
        }

        private void MoveToITOPOD()
        {
            if (!Settings.GlobalEnabled)
                return;

            //if (_questManager.IsQuesting() >= 0)
            //    return;

            if (Settings.CombatEnabled)
                return;

            if (Settings.DoGoldSwap)
                return;

            //If we're not in ITOPOD, move there if its set
            //if (Character.adventureController.zone >= 1000 || !Settings.AutoQuestITOPOD) return;
            Log($"Moving to ITOPOD to idle.");
            //_combManager.MoveToZone(1000);
        }

        private void DumpEquipped()
        {
            //var list = new List<int>
            //{
            //    Character.inventory.head.id,
            //    Character.inventory.chest.id,
            //    Character.inventory.legs.id,
            //    Character.inventory.boots.id,
            //    Character.inventory.weapon.id
            //};

            //if (Character.inventoryController.weapon2Unlocked())
            //{
            //    list.Add(Character.inventory.weapon2.id);
            //}

            //foreach (var acc in Character.inventory.accs)
            //{
            //    list.Add(acc.id);
            //}

            //list.RemoveAll(x => x == 0);

            //Log($"Equipped Items: [{string.Join(", ", list.Select(x => x.ToString()).ToArray())}]");
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

        public void SetResnipe()
        {
            if (Settings.ResnipeTime == 0 && !Settings.GoldCBlockMode) return;

            if (Settings.GoldCBlockMode)
            {
                //var furthest = ZoneHelpers.GetMaxReachableZone(false);
                //if (furthest > _furthestZone)
                //{
                //    Settings.DoGoldSwap = true;
                //    _furthestZone = furthest;
                //}

                return;
            }

            //if (Math.Abs(Character.rebirthTime.totalseconds - Settings.ResnipeTime) <= 1)
            //{
            //    Settings.DoGoldSwap = true;
            //}
        }

        public void ShowBoostProgress()
        {
            //var boostSlots = _invManager.GetBoostSlots(Character.inventory.GetConvertedInventory().ToArray());
            //try
            //{
            //    _invManager.ShowBoostProgress(boostSlots);
            //}
            //catch (Exception e)
            //{
            //    Log(e.Message);
            //    Log(e.StackTrace);
            //}

        }

        public void OnApplicationQuit()
        {
            Loader.Unload();
        }
    }
}
