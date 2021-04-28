using System;
using System.Collections.Generic;
using System.Linq;

namespace NGUIndustriesInjector
{
    class MaterialState
    {
        private const int WANT_SECONDS = 3600 * 10;

        private static List<MaterialState> allMaterials;
        private static Player player;
        private static TrackerController tracker;

        private readonly MaterialData materialData;
        private readonly BuildingProperties properties;

        internal BuildingType BuildingType { get; }
        internal int BuildingId { get; set; }
        internal int BuildNumber { get; set; }
        internal int BuildPercent { get; set; }

        private Func<MaterialState, bool> DeleteFunction;

        internal MaterialState(BuildingType buildingType)
        {
            this.BuildingType = buildingType;
            this.BuildingId = (int)buildingType;
            this.materialData = player.materials.materials[BuildingId];
            this.properties = tracker.buildingProperties.properties[BuildingId];
        }

        internal static void SetState(List<MaterialState> allMaterials, Player player)
        {
            MaterialState.allMaterials = allMaterials;
            MaterialState.player = player;
            tracker = player.factoryController.tracker;
        }

        internal bool Unlocked
        {
            get => materialData.unlocked;
        }

        internal long Amount
        {
            get => materialData.amount;
        }

        internal double Gain
        {
            get => tracker.theoreticalGainLoss[BuildingId];
        }

        internal double BaseTime
        {
            get => properties.baseTime;
        }

        internal double MinTime
        {
            get => properties.minTime;
        }

        internal double PerSecond
        {
            get => materialData.largestProduction / properties.baseTime;
        }

        internal int BuildingCount
        {
            get => tracker.buildingCounts[BuildingId];
        }

        internal string Name
        {
            get => properties.name;
        }

        internal bool IsJuice
        {
            get => Name.Contains("Juice");
        }

        internal double ShouldHave
        {
            get
            {
                //var prio = Main.Settings.PriorityBuildings.Find(b => (int)b.x == BuildingId);
                //if (prio != null)
                //   return prio.y;

                return PerSecond * WANT_SECONDS;
            }
        }

        internal double HavePercent
        {
            get
            {
                if (ShouldHave > 0)
                    return Amount / ShouldHave * 100;
                return 0;
            }
        }

        internal List<BuildingType> ToDelete
        {
            get
            {
                var data = from entry in allMaterials where entry.BuildingCount > 0 && DeleteFunction.Invoke(entry) select entry.BuildingType;
                return data.ToList();
            }
        }

        internal void CalculateState()
        {
            BuildNumber = 0;
            BuildPercent = 0;
            DeleteFunction = other => other.HavePercent > BuildPercent && other.BuildNumber == 0;

            Main.Log($"{this}: Calculating");

            if (!Unlocked)
                return;

            if (PerSecond <= 0)
                return;

            if (Main.Settings.FactoryDontStarve && HavePercent < 10 && Gain < 0)
            {
                BuildNumber = -(int)(Gain / PerSecond / Math.Max(1, HavePercent));
                BuildNumber = Math.Max(1, Math.Min(BuildNumber, 30));
                BuildPercent = 5;
                DeleteFunction = other => (other.HavePercent > BuildPercent && other.Gain - other.PerSecond > 0) || other.HavePercent > 20;
                Main.Log($"{this}: Adding DONT STARVE to SET < 10% and gain < 0 to build {BuildNumber}");
                return;
            }

            var prio = Main.Settings.PriorityBuildings.Find(b => (int)b.type == BuildingId);
            if (prio != null && (int)prio.type == BuildingId)
            {
                var needed = prio.want;
                Main.Log($"{this}: Is on prio list with needed {needed}");
                if (needed > Amount)
                {
                    BuildNumber = 1;
                    BuildPercent = 100;
                    Main.Log($"{this}: Adding PRIO MODE build neede {needed} > have {Amount}");
                    return;
                }
            }

            if (Main.Settings.FactoryBuildStandard)
            {
                if (HavePercent < 10 && BuildingCount < 3)
                {
                    BuildNumber = 1;
                    BuildPercent = 200;
                    Main.Log($"{this}: Adding BUILD STANDARD to SET < 10% and less than 3 instances");
                    return;
                }
                else if (HavePercent < 1)
                {
                    BuildNumber = 1;
                    BuildPercent = 200;
                    Main.Log($"{this}: Adding BUILD STANDARD to SET < 1%");
                    return;
                }
                else if (HavePercent < 100 && Gain <= 0)
                {
                    BuildNumber = 1;
                    BuildPercent = 200;
                    Main.Log($"{this}: Adding BUILD STANDARD to SET < 100% and gain <= 0");
                    return;
                }
            }
        }

        public override string ToString()
        {
            return $"[{BuildingId} {Name}: {ShouldHave} % {HavePercent} build {BuildNumber} on % {BuildPercent}]";
        }
    }
}
