﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NGUInjector
{
    internal static class DiggerManager
    {
        private static int[] _savedDiggers;
        internal static LockType CurrentLock { get; set; }
        private static readonly int[] TitanDiggers = { 0, 3, 8, 11 };
        private static readonly int[] YggDiggers = {8, 11};

        internal static bool CanSwap()
        {
            return CurrentLock == LockType.None;
        }

        internal static void TryTitanSwap()
        {
            if (CurrentLock == LockType.Titan)
            {
                if (LoadoutManager.TitansSpawningSoon())
                    return;

                RestoreDiggers();
                ReleaseLock();
                return;
            }

            if (LoadoutManager.TitansSpawningSoon())
            {
                CurrentLock = LockType.Titan;
                SaveDiggers();
                EquipDiggers(TitanDiggers);
            }
        }

        internal static bool TryYggSwap()
        {
            if (CurrentLock == LockType.Titan)
                return false;

            CurrentLock = LockType.Yggdrasil;
            SaveDiggers();
            EquipDiggers(YggDiggers);
            return true;
        }

        internal static void ReleaseLock()
        {
            CurrentLock = LockType.None;
        }

        internal static void SaveDiggers()
        {
            var temp = new List<int>();
            for (var i = 0; i < Main.Character.diggers.diggers.Count; i++)
            {
                if (Main.Character.diggers.diggers[i].active)
                {
                    temp.Add(i);
                }
                    
            }

            _savedDiggers = temp.ToArray();
        }

        internal static void EquipDiggers(int[] diggers)
        {
            Main.Log($"Equipping Diggers: {string.Join(",", diggers.Select(x => x.ToString()).ToArray())}");
            Main.Character.allDiggers.clearAllActiveDiggers();
            var sorted = diggers.OrderByDescending(x => x).ToArray();
            for (var i = 0; i < sorted.Length; i++)
            {
                if (Main.Character.diggers.diggers[i].maxLevel <= 0)
                    continue;
                Main.Character.allDiggers.setLevelMaxAffordable(sorted[i]);
            }
        }

        internal static void RecapDiggers()
        {
            for (var i = 0; i < Main.Character.diggers.diggers.Count; i++)
            {
                if (Main.Character.diggers.diggers[i].active)
                {
                    Main.Character.allDiggers.setLevelMaxAffordable(i);
                }
            }
        }

        internal static void RestoreDiggers()
        {
            Main.Character.allDiggers.clearAllActiveDiggers();
            EquipDiggers(_savedDiggers);
        }
    }
}
