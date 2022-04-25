using Aerolt.Cheats;
using Aerolt.Classes;
using Aerolt.Menu;
using Aerolt.Menu.Tabs;
using Aerolt.Utilities;
using RoR2;
using System.Linq;
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
            SpawnerTab.interactableSpawnCards = UnityEngine.Object.FindObjectsOfType<InteractableSpawnCard>().ToList();
        }

        public static void GetCharacterData(CharacterBody body)
        {
            GC.getCharacter();
            LM.GetPlayers();
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