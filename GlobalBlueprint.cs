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
}
