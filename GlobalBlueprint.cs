using System;
using System.Collections.Generic;
using System.Linq;

namespace NGUIndustriesInjector
{
    public class GlobalBlueprint
    {
        public GlobalBlueprint()
        {

        }

        public GlobalBlueprint(string name, List<MapData> mapData = null)
        {
            Name = name;
            Maps = new List<BlueprintMap>();
            if (mapData == null)
                return;

            int mapId = 0;
            foreach (var map in mapData.Where(map => map.unlocked))
            {
                Maps.Add(new BlueprintMap(map, mapId++));
            }
        }

        public string Name { get; set; }

        public List<BlueprintMap> Maps { get; set; }

        public void Load(Player player)
        {
            if (!Maps?.Any() ?? true)
            {
                return;
            }

            var originalMapId = player.factoryController.curMapID;
            foreach (var map in Maps)
            {
                player.factoryController.setNewMapID(map.MapIndex);

                map.BlueprintTiles?.ForEach(tile =>
                    player.factoryController.doSetTile(tile.Index, tile.BuildingType, tile.TileDirection));
            }

            player.factoryController.setNewMapID(originalMapId);
        }
    }

    public class BlueprintMap
    {
        public BlueprintMap()
        {

        }

        public BlueprintMap(MapData mapData, int mapId)
        {
            MapIndex = mapId;
            BlueprintTiles = new List<BlueprintTile>();
            mapData.beacons?.ForEach(beacon => BlueprintTiles.Add(new BlueprintTile(beacon)));
            mapData.buildings?.ForEach(beacon => BlueprintTiles.Add(new BlueprintTile(beacon)));
            mapData.labs?.ForEach(beacon => BlueprintTiles.Add(new BlueprintTile(beacon)));
        }

        public int MapIndex { get; set; }

        public List<BlueprintTile> BlueprintTiles { get; set; }
    }

    public class BlueprintTile
    {
        public BlueprintTile()
        {

        }

        public BlueprintTile(int index, BuildingType buildingType, TileDirection tileDirection = TileDirection.Up)
        {
            Index = index;
            BuildingType = buildingType;
            TileDirection = tileDirection;
        }

        public BlueprintTile(TileData tileData)
        {
            Index = tileData.index;
            BuildingType = tileData.building;
            TileDirection = tileData.direction;
        }

        public int Index { get; set; }
        public BuildingType BuildingType { get; set; }
        public TileDirection TileDirection { get; set; }
    }

    public class GlobalBlueprintTrigger
    {
        public GlobalBlueprintTrigger()
        {

        }

        public GlobalBlueprintTrigger(string blueprintName, ResourceType resourceType, int materialCount)
        {
            BlueprintName = blueprintName;
            ResourceType = resourceType;
            MaterialCount = materialCount;
        }

        public string BlueprintName { get; set; }

        public int MaterialCount { get; set; }

        public ResourceType ResourceType { get; set; }

        public void CheckTrigger(Player player)
        {
            if (MaterialCount == 0 || ResourceType == ResourceType.None)
            {
                return;
            }

            bool trigger = false;
            if ((int)this.ResourceType >= (int)ResourceType.Unobtanium &&
                (int)this.ResourceType <= (int)ResourceType.SURPRISE)
            {
                var resources = player.experiments.resources;
                var resourceIndex = (int)this.ResourceType - 1000;

                trigger = resources[resourceIndex] >= this.MaterialCount;
            }
            else 
            {
                trigger = player.materials.materials[(int)ResourceType].amount >= this.MaterialCount;
            }

            if (trigger)
            {
                Main.Debug($"Successful global blueprint trigger of {this.BlueprintName}", "ManageGlobalBlueprintTriggers");

                Main.Settings.GlobalBlueprintTriggers.Remove(this);
                if (!Main.Settings.GlobalBlueprintTriggers.Any())
                {
                    var name = Main.Settings.GlobalBlueprints.FirstOrDefault()?.Name ?? "Default";
                    Main.Settings.GlobalBlueprintTriggers.Add(new GlobalBlueprintTrigger(name, ResourceType.None, 0));
                }

                var blueprint = Main.Settings.GlobalBlueprints.FirstOrDefault(bp => bp.Name == this.BlueprintName);
                if (blueprint == null)
                {
                    Main.Debug("Invalid state. Unable to find blueprint by name.", "ManageGlobalBlueprintTriggers");
                    return;
                }

                blueprint.Load(player);
                Main.Settings.SaveSettings();
            }
        }

        internal void Deconstruct(out string blueprintName, out ResourceType buildingType, out int materialCount)
        {
            blueprintName = BlueprintName;
            buildingType = ResourceType;
            materialCount = MaterialCount;
        }
    }

    public enum ResourceType
    {
        None = 0,
        Unobtanium = 1000,
        Phlebtonium = 1001,
        Quantum = 1002,
        IDKWhatTheHellThisIs = 1003,
        SURPRISE = 1004,
        IronOre = 1,
        CopperOre = 2,
        IronBar = 3,
        CopperBar = 4,
        BoxBeacon = 5,
        Lab1 = 6,
        ThinkJuice1 = 7,
        CardboardOre = 8,
        CardboardBar = 9,
        GlueOre = 10,
        GlueBar = 11,
        ShittyCog = 12,
        ShittyWires = 13,
        Staples = 14,
        Chip1 = 15,
        ThinkJuice2 = 16,
        BoxBeaconProd = 17,
        BoxBeaconEff = 18,
        MetalFrame = 19,
        MetalWheels = 20,
        ShittyHardDrive = 21,
        ShittyRoboBrain = 22,
        SteelOre = 23,
        SteelBar = 24,
        SteelWool = 25,
        ShittyReactor = 26,
        ThinkJuice3 = 27,
        ThinkJuice4 = 28,
        Rocket = 29,
        DecentWiring = 30,
        SuperGlue = 31,
        BasicFuel = 32,
        KillBotV1 = 33,
        SteelVest = 34,
        SteelBlade = 35,
        Chip2 = 36,
        MeatOre = 37,
        MeatBar = 38,
        BoneOre = 39,
        BoneBar = 40,
        BoneBeam = 41,
        BeatingHeart = 42,
        FleshJuice1 = 43,
        KnightBeaconSpd = 44,
        KnightBeaconProd = 45,
        KnightBeaconEff = 46,
        boneFrame = 47,
        bioReactor = 48,
        roboButt = 49,
        boneTreads = 50,
        FleshJuice2 = 51,
        CombatJuice1 = 52,
        CombatJuice2 = 53,
        CombatJuice3 = 54,
        CombatJuice4 = 55,
        CombatJuice5 = 56,
        OffenseButt = 57,
        DefenseButt = 58,
        BasicShield = 59,
        ButtGun = 60,
        BioChip = 61,
        FleshJuice3 = 62,
        EngineFlesh = 63,
        FartFuel = 64,
        BioComputer = 65,
        FleshRocket = 66,
        FleshJuice4 = 67,
        TutorialArmy = 68,
        ArrowBeaconSpd = 69,
        ArrowBeaconProd = 70,
        ArrowBeaconEff = 71,
        TechOre = 72,
        TechBar = 73,
        PlasticOre = 74,
        PlasticBar = 75,
        TechAlloy = 76,
        TechFrame = 77,
        TechCPU = 78,
        GiantCoil = 79,
        RailGun = 80,
        DefensiveCover = 81,
        SHARTReactor = 82,
        TechDroneBase = 83,
        GunDrone = 84,
        ShieldDrone = 85,
        TechJuice1 = 86,
        TechJuice2 = 87,
        TechJuice3 = 88,
        TechJuice4 = 89,
        Nacelle = 90,
        TronneComputer = 91,
        CybercornHead = 92,
        TronneRocket = 93,
        CandyOre = 94,
        CandyBar = 95,
        ChocoOre = 96,
        ChocoBar = 97,
        CandyCompound = 98,
        ChocoCompound = 99,
        CandyJuice1 = 100,
        Nitro = 101,
        LicoriceWire = 102,
        ConfectFrame = 103,
        TinderSurprise = 104,
        NougatCPU = 105,
        CandyJuice2 = 106,
        FondueReactor = 107,
        Chockets = 108,
        MintMechBody = 109,
        CinnamonFlamethrower = 110,
        CandyJuice3 = 111,
        MintyMech = 112,
        CandyComputer = 113,
        CandyShip = 114,
        MGHSC = 115,
        CandyJuice4 = 116,
        WallBeaconSpeed = 117,
        WallBeaconProd = 118,
        WallBeaconEff = 119,
        DonutBeaconSpeed = 120,
        DonutBeaconProd = 121,
        DonutBeaconEff = 122,
    }
}