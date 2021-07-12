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

        public GlobalBlueprintTrigger(string blueprintName, BuildingType buildingType, int materialCount)
        {
            BlueprintName = blueprintName;
            BuildingType = buildingType;
            MaterialCount = materialCount;
        }

        public string BlueprintName { get; set; }

        public int MaterialCount { get; set; }

        public BuildingType BuildingType { get; set; }

        public void Trigger(Player player)
        {
            if (MaterialCount == 0 || BuildingType == BuildingType.None)
            {
                return;
            }

            if (player.materials.materials[(int)BuildingType].amount >= this.MaterialCount)
            {
                Main.Debug($"Successful global blueprint trigger of {this.BlueprintName}", "ManageGlobalBlueprintTriggers");

                Main.Settings.GlobalBlueprintTriggers.Remove(this);
                if (!Main.Settings.GlobalBlueprintTriggers.Any())
                {
                    var name = Main.Settings.GlobalBlueprints.FirstOrDefault()?.Name ?? "Default";
                    Main.Settings.GlobalBlueprintTriggers.Add(new GlobalBlueprintTrigger(name, BuildingType.None, 0));
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

        internal void Deconstruct(out string blueprintName, out BuildingType buildingType, out int materialCount)
        {
            blueprintName = BlueprintName;
            buildingType = BuildingType;
            materialCount = MaterialCount;
        }
    }
}
