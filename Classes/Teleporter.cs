using Aerolt.Attributes;
using RoR2;
using UnityEngine;

namespace Aerolt.Cheats
{
    class Teleporter
    {
        public static void InstaTeleporter()
        {
            if (TeleporterInteraction.instance)
            {
                typeof(HoldoutZoneController).GetProperty("charge").SetValue(TeleporterInteraction.instance.holdoutZoneController, 1f);
                Debug.Log("Teleporter Ready");
            }
        }
        public static void skipStage()
        {
            Run.instance.AdvanceStage(Run.instance.nextStageScene);
            Debug.Log("RoRCheats : Skipped Stage");
        }
        public static void addMountain()
        {
            TeleporterInteraction.instance.AddShrineStack();
        }
        public static void SpawnPortals(string portal)
        {
            switch (portal)
            {
                case "gold":
                    RoR2.Chat.AddMessage("<color=yellow>Spawned Gold Portal</color>");
                    TeleporterInteraction.instance.shouldAttemptToSpawnGoldshoresPortal = true;
                    break;
                case "newt":
                    RoR2.Chat.AddMessage("<color=blue>Spawned Newt Portal</color>");
                    TeleporterInteraction.instance.shouldAttemptToSpawnShopPortal = true;
                    break;
                case "blue":
                    RoR2.Chat.AddMessage("<color=cyan>Spawned Celestial Portal</color>");
                    TeleporterInteraction.instance.shouldAttemptToSpawnMSPortal = true;
                    break;
                case "all":
                    RoR2.Chat.AddMessage("<color=red>Spawned All Portal</color>");
                    TeleporterInteraction.instance.shouldAttemptToSpawnGoldshoresPortal = true;
                    TeleporterInteraction.instance.shouldAttemptToSpawnShopPortal = true;
                    TeleporterInteraction.instance.shouldAttemptToSpawnMSPortal = true;
                    break;
            }
        }
    }
}
