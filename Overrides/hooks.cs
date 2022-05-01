using Aerolt.Cheats;
using Aerolt.Classes;
using Aerolt.Menu;
using Aerolt.Menu.Tabs;
using Aerolt.Utilities;
using HarmonyLib;
using RoR2;
using RoR2.UI;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using GC = Aerolt.Classes.GC;

namespace Aerolt.Overrides
{
    [HarmonyPatch]
    public class H
    {

        public static void GetData(SceneDirector director)
        {
            ESP.barrelInteractions = UnityEngine.Object.FindObjectsOfType<BarrelInteraction>().ToList();
            ESP.purchaseInteractions = UnityEngine.Object.FindObjectsOfType<PurchaseInteraction>().ToList();
            ESP.secretButtons = UnityEngine.Object.FindObjectsOfType<PressurePlateController>().ToList();
            ESP.scrappers = UnityEngine.Object.FindObjectsOfType<ScrapperController>().ToList();
            ESP.multiShops = UnityEngine.Object.FindObjectsOfType<MultiShopController>().ToList();

            foreach(var master in MasterCatalog.allAiMasters)
                G.MonsterButtons.Add(new GUIContent(master.name));
            
            G.InteractablesSpawnCards = director.GenerateInteractableCardSelection().choices.Where(x => x.value != null).Select(x => x.value.spawnCard).ToList();
            foreach (var cards in G.InteractablesSpawnCards)
                G.InteractableButtons.Add(new GUIContent(cards.prefab.name.ToString()));
            
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
            try
            {
                //thanks Bubbet ♥
                var cursorGrabber = new GameObject("CursorGrabber");
                cursorGrabber.transform.SetParent(__instance.mainUIPanel.transform);
                cursorGrabber.SetActive(false);

                cursorGrabber.AddComponent<MPEventSystemLocator>();
                cursorGrabber.AddComponent<CursorOpener>();

                G.CursorGrabber = cursorGrabber;
            }
            catch(Exception ex)
            {
                T.Log(LogLevel.Error,ex.ToString());
            }
        }
        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Items.getItemNames();
            Items.getEquipmentNames();

            if (scene.name.Contains("lobby") || scene.name.Contains("title"))
                T.resetMenu();
        }


    }
}