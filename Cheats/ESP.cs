using Aerolt.Attributes;
using Aerolt.Classes;
using Aerolt.Menu;
using Aerolt.Utilities;
using Aerolt.Overrides;
using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;
using Assets = Aerolt.Utilities.AssetUtilities;

namespace Aerolt.Cheats
{
    [Comp]
    public class ESP : MonoBehaviour
    {
        public static List<PurchaseInteraction> purchaseInteractions = new List<PurchaseInteraction>();
        public static List<BarrelInteraction> barrelInteractions = new List<BarrelInteraction>();
        public static List<PressurePlateController> secretButtons = new List<PressurePlateController>();
        public static List<ScrapperController> scrappers = new List<ScrapperController>();
        public static List<MultiShopController> multiShops = new List<MultiShopController>();

        public static List<HurtBox> hurtBoxes;

        void OnGUI()
        {
            try
            {
                if (G.Settings.espoptions.Enabled)
                {
                   // Mobs();
                    if (G.Settings.espoptions.showTeleporter)
                        ESP.showTeleporter();
                    if (G.Settings.espoptions.showChest)
                        ESP.showChest();
                    if (G.Settings.espoptions.showScrappers)
                        ESP.showScrapper();
                    if (G.Settings.espoptions.showPressurePlates)
                        ESP.showPlate();
                    if (G.Settings.espoptions.showBarrels)
                        ESP.showBarrel();
                    if (G.Settings.espoptions.showEnemies)
                        showMobs();
                    if (G.Settings.espoptions.showMultiShops)
                        showMobs();
                }
            }
            catch (NullReferenceException)
            {

            }
            
        }
        public static void showMobs()
        {
            for (int i = 0; i < hurtBoxes.Count; i++)
            {
                var mob = HurtBox.FindEntityObject(hurtBoxes[i]);
                if (mob)
                {
                    Vector3 MobPosition = Camera.main.WorldToScreenPoint(mob.transform.position);
                    var MobBoundingVector = new Vector3(MobPosition.x, MobPosition.y, MobPosition.z);
                    float distanceToMob = Vector3.Distance(Camera.main.transform.position, mob.transform.position);
                    if (MobBoundingVector.z > 0.01)
                    {
                        float width = 100f * (distanceToMob / 100);
                        if (width > 125)
                        {
                            width = 125;
                        }
                        float height = 100f * (distanceToMob / 100);
                        if (height > 125)
                        {
                            height = 125;
                        }
                        string mobName = mob.name.Replace("Body(Clone)", "");
                        int mobDistance = (int)distanceToMob;
                        string mobBoxText = $"{mobName}\n{mobDistance}m";
                        T.DrawESPLabel(mob.transform.position, Color.yellow, Color.clear, mobBoxText);
                    }
                }
            }
        }
        public static void showTeleporter()
        {
            if (TeleporterInteraction.instance)
            {
                var teleporterInteraction = TeleporterInteraction.instance;
                float distanceToObject = Vector3.Distance(Camera.main.transform.position, teleporterInteraction.transform.position);
                int distance = (int)distanceToObject;
                String friendlyName = "Teleporter";

                Color teleporterColor =
                    teleporterInteraction.isIdle ? Color.magenta :
                    teleporterInteraction.isIdleToCharging || teleporterInteraction.isCharging ? Color.yellow :
                    teleporterInteraction.isCharged ? Color.green : Color.yellow;



                string status = "" + (
                    teleporterInteraction.isIdle ? "Idle" :
                    teleporterInteraction.isCharging ? "Charging" :
                    teleporterInteraction.isCharged ? "Charged" :
                    teleporterInteraction.isActiveAndEnabled ? "Idle" :
                    teleporterInteraction.isIdleToCharging ? "Idle-Charging" :
                    teleporterInteraction.isInFinalSequence ? "Final-Sequence" :
                    "???");

                string boxText = $"{friendlyName}\n{status}\n{distance}m";
                T.DrawESPLabel(teleporterInteraction.transform.position, teleporterColor, Color.clear, boxText);
                
            }
         
        }

        public static void showChest()
        {
            foreach (PurchaseInteraction purchaseInteraction in purchaseInteractions)
            {
                if (purchaseInteraction.available)
                {
                    var chest = purchaseInteraction?.gameObject.GetComponent<ChestBehavior>();

                    float distanceToObject = Vector3.Distance(Camera.main.transform.position, purchaseInteraction.transform.position);

                    int distance = (int)distanceToObject;
                    String friendlyName = purchaseInteraction.GetDisplayName();
                    int cost = purchaseInteraction.cost;
                    string boxText = $"{friendlyName}\n${cost}\n{distance}m";
                    T.DrawESPLabel(purchaseInteraction.transform.position, Colors.GetColor("Chest"), Color.clear, boxText);
                   
                }
            }
        }
        public static void showMultiShop()
        {
            foreach (MultiShopController multiShop in multiShops)
            {
                if (multiShop.available)
                {
                    float distanceToObject = Vector3.Distance(Camera.main.transform.position, multiShop.terminalPrefab.transform.position);

                    int distance = (int)distanceToObject;
                    String friendlyName = "MultiShop";
                    int cost = multiShop.cost;
                    string boxText = $"{friendlyName}\n${cost}\n{distance}m";
                    T.DrawESPLabel(multiShop.transform.position, Colors.GetColor("Chest"), Color.clear, boxText);
                }
            }
        }
        public static void showPlate()
        {
            foreach (PressurePlateController secretButton in secretButtons)
            {
                if (secretButton)
                {
                    string friendlyName = "Secret Button";

                    float distance = (int)Vector3.Distance(Camera.main.transform.position, secretButton.transform.position);
                    string boxText = $"{friendlyName}\n{distance}m";
                    T.DrawESPLabel(secretButton.transform.position, Colors.GetColor("Secret_Plates"), Color.clear, boxText);
                    
                }
            }
        }
        public static void Mobs()
        {
            for (int i = 0; i < hurtBoxes.Count; i++)
            {
                var mob = HurtBox.FindEntityObject(hurtBoxes[i]);
                if (mob)
                {
                    Vector3 MobPosition = Camera.main.WorldToScreenPoint(mob.transform.position);
                    var MobBoundingVector = new Vector3(MobPosition.x, MobPosition.y, MobPosition.z);
                    float distanceToMob = Vector3.Distance(Camera.main.transform.position, mob.transform.position);
                    if (MobBoundingVector.z > 0.01)
                    {
                        float width = 100f * (distanceToMob / 100);
                        if (width > 125)
                        {
                            width = 125;
                        }
                        float height = 100f * (distanceToMob / 100);
                        if (height > 125)
                        {
                            height = 125;
                        }
                        string mobName = mob.name.Replace("Body(Clone)", "");
                        int mobDistance = (int)distanceToMob;
                        string mobBoxText = $"{mobName}\n{mobDistance}m";
                        
                        T.DrawOutlineLabel(new Vector2(MobBoundingVector.x - width / 2, (float)Screen.height - MobBoundingVector.y - height / 2), new Color32(255, 0, 0, 255), new Color32(255, 0, 0, 255), mobName);
                    }
                }
            }
        }

        public static void showScrapper()
        {
            foreach (ScrapperController scrapper in scrappers)
            {
                if (scrapper)
                {
                    string friendlyName = "Scrapper";

                    float distance = (int)Vector3.Distance(Camera.main.transform.position, scrapper.transform.position);
                    string boxText = $"{friendlyName}\n{distance}m";
                    T.DrawESPLabel(scrapper.transform.position, Colors.GetColor("Scrappers"), Color.clear, boxText);
                }
            }
        }

        public static void showBarrel()
        {
            foreach (BarrelInteraction barrel in barrelInteractions)
            {
                if (!barrel.Networkopened)
                {
                    string friendlyName = "Barrel";

                    float distance = (int)Vector3.Distance(Camera.main.transform.position, barrel.transform.position);
                    string boxText = $"{friendlyName}\n{distance}m";
                    T.DrawESPLabel(barrel.transform.position, Colors.GetColor("Barrels"), Color.clear, boxText);
                    
                }
            }
        }
    }
}
