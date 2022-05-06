using Aerolt.Classes;
using Aerolt.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Aerolt.Menu.Tabs
{
    public class EspTab
    {
       
        private static Vector2 scrollPosition;
        private static Vector2 OptionsScrollPosition;
        public static string SelectedColorIdentifier = "";
        public static void Tab()
        {
            GUILayout.Space(0);
            GUILayout.BeginArea(new Rect(10, 35, 260, 400), style: "box", text: "ESP Options");
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            G.Settings.espoptions.Enabled = GUILayout.Toggle(G.Settings.espoptions.Enabled, "Toggle ESP");
            GUILayout.Space(20);
            G.Settings.espoptions.showTeleporter = GUILayout.Toggle(G.Settings.espoptions.showTeleporter, "Show Teleporter");
            G.Settings.espoptions.showChest = GUILayout.Toggle(G.Settings.espoptions.showChest, "Show Chests");
            G.Settings.espoptions.showScrappers = GUILayout.Toggle(G.Settings.espoptions.showScrappers, "Show Scrappers");
            G.Settings.espoptions.showPressurePlates = GUILayout.Toggle(G.Settings.espoptions.showPressurePlates, "Show Pressure Plates");
            G.Settings.espoptions.showBarrels = GUILayout.Toggle(G.Settings.espoptions.showBarrels, "Show Barrels");
            G.Settings.espoptions.showMultiShops = GUILayout.Toggle(G.Settings.espoptions.showMultiShops, "Show MultiShops");
            G.Settings.espoptions.showEnemies = GUILayout.Toggle(G.Settings.espoptions.showEnemies, "Show Enemies");
            
            GUILayout.EndScrollView();
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(280, 35, 260, 400), style: "box", text: "Colors");
            OptionsScrollPosition = GUILayout.BeginScrollView(OptionsScrollPosition);

            List<string> keys = new List<string>(G.Settings.GlobalOptions.GlobalColors.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                string key = keys[i];
                Color32 color = Colors.GetColor(key);
                string s = $"<color=#{Colors.ColorToHex(color)}>{key.Replace("_", " ")}</color>";

                if (SelectedColorIdentifier == key)
                {
                    if (GUILayout.Button(s, style: "SelectedButton"))
                        SelectedColorIdentifier = "";
                    GUILayout.BeginVertical(style: "SelectedButtonDropdown");
                    Color32 c = color;
                    Color32 cc = new Color32() { a = 255 };
                    GUILayout.Label("R: " + c.r);
                    cc.r = (byte)GUILayout.HorizontalSlider(c.r, 0, 255);
                    GUILayout.Space(2);

                    GUILayout.Label("G: " + c.g);
                    cc.g = (byte)GUILayout.HorizontalSlider(c.g, 0, 255);
                    GUILayout.Space(2);

                    GUILayout.Label("B: " + c.b);
                    cc.b = (byte)GUILayout.HorizontalSlider(c.b, 0, 255);
                    G.Settings.GlobalOptions.GlobalColors[key] = cc;
                    GUILayout.EndVertical();
                }
                else if (GUILayout.Button(s))
                    SelectedColorIdentifier = key;
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();

        }
    }
}
