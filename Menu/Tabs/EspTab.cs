using Aerolt.Classes;
using UnityEngine;

namespace Aerolt.Menu.Tabs
{
    public class EspTab
    {
       
        private static Vector2 scrollPosition;
        public static void Tab()
        {
            GUILayout.Space(0);
            GUILayout.BeginArea(new Rect(10, 35, 530, 400), style: "box", text: "ESP Options");
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            G.Settings.espoptions.Enabled = GUILayout.Toggle(G.Settings.espoptions.Enabled, "Toggle ESP");
            GUILayout.Space(20);
            G.Settings.espoptions.showTeleporter = GUILayout.Toggle(G.Settings.espoptions.showTeleporter, "Show Teleporter");
            G.Settings.espoptions.showChest = GUILayout.Toggle(G.Settings.espoptions.showChest, "Show Chests");
            G.Settings.espoptions.showScrappers = GUILayout.Toggle(G.Settings.espoptions.showScrappers, "Show Scrappers");
            G.Settings.espoptions.showPressurePlates = GUILayout.Toggle(G.Settings.espoptions.showPressurePlates, "Show Pressure Plates");
            G.Settings.espoptions.showBarrels = GUILayout.Toggle(G.Settings.espoptions.showBarrels, "Show Barrels");
            G.Settings.espoptions.showMultiShops = GUILayout.Toggle(G.Settings.espoptions.showMultiShops, "Show MultiShops");

            GUILayout.EndScrollView();
            GUILayout.EndArea();
            
            if (G.Settings.AimbotOptions.AimlockKey == KeyCode.None)
            {
                Event e = Event.current;
                G.Settings.AimbotOptions.AimlockKey = e.keyCode;
            }
        }
    }
}
