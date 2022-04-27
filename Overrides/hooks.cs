using Aerolt.Cheats;
using Aerolt.Classes;
using Aerolt.Menu;
using Aerolt.Menu.Tabs;
using Aerolt.Utilities;
using HarmonyLib;
using RoR2;
using RoR2.UI;
using System.Linq;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using GC = Aerolt.Classes.GC;

namespace Aerolt.Overrides
{
    public class H
    {


        public static void GetESPData(SceneDirector obj)
        {
            ESP.barrelInteractions = UnityEngine.Object.FindObjectsOfType<BarrelInteraction>().ToList();
            ESP.purchaseInteractions = UnityEngine.Object.FindObjectsOfType<PurchaseInteraction>().ToList();
            ESP.secretButtons = UnityEngine.Object.FindObjectsOfType<PressurePlateController>().ToList();
            ESP.scrappers = UnityEngine.Object.FindObjectsOfType<ScrapperController>().ToList();
            //SpawnerTab.interactableSpawnCards = UnityEngine.Object.FindObjectsOfType<InteractableSpawnCard>().ToList();
        }

        public static void GetCharacterData(CharacterBody body)
        {
            GC.getCharacter();
        }

        public static void PlayerListUpdate(NetworkConnection conn)
        {
            LM.GetPlayers();
        }

        [HarmonyPostfix, HarmonyPatch(typeof(HUD), nameof(HUD.Awake))]
        public static void Postfix(HUD __instance)
        {
            
        }
        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ESP.barrelInteractions = UnityEngine.Object.FindObjectsOfType<BarrelInteraction>().ToList();
            ESP.purchaseInteractions = UnityEngine.Object.FindObjectsOfType<PurchaseInteraction>().ToList();
            ESP.secretButtons = UnityEngine.Object.FindObjectsOfType<PressurePlateController>().ToList();
            ESP.scrappers = UnityEngine.Object.FindObjectsOfType<ScrapperController>().ToList();
            Items.getItemNames();
            Items.getEquipmentNames();
            if (scene.name.Contains("lobby") || scene.name.Contains("title"))
                T.resetMenu();
        }


    }
}