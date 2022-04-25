using Aerolt.Classes;
using Aerolt.Cheats;
using RoR2;
using UnityEngine;

namespace Aerolt.Menu.Tabs
{
    public class TeleporterTab
    {
        public static void Tab()
        {
            GUILayout.Space(0);
            GUILayout.BeginArea(new Rect(10, 35, 530, 400), style: "box", text: "Teleporter");

            if (GUILayout.Button("Skip Stage"))
                Teleporter.skipStage();
            if (GUILayout.Button("Insta Charge Teleporter"))
                Teleporter.InstaTeleporter();
            GUILayout.Space(20);
            try
            {
                if (GUILayout.Button("Add Mountain : Current Ammount " + TeleporterInteraction.instance.shrineBonusStacks))
                    Teleporter.addMountain();
            }
            catch
            {
                if (GUILayout.Button("Remember to buy stuff from the newt :D"))
                    Debug.LogWarning("I dont do anything just here for the newt");
            }

            GUILayout.Space(20);
            if (GUILayout.Button("Spawn Gold Portal"))
                Teleporter.SpawnPortals("gold");
            if (GUILayout.Button("Spawn Newt Portal"))
                Teleporter.SpawnPortals("newt");
            if (GUILayout.Button("Spawn Celestial Portal"))
                Teleporter.SpawnPortals("blue");
            if (GUILayout.Button("Spawn All Portal"))
                Teleporter.SpawnPortals("all");
            GUILayout.EndArea();
        }
    }
}
