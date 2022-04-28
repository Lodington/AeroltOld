﻿using Aerolt.Cheats;
using Aerolt.Utilities;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Aerolt.Menu.Tabs
{
    public class SpawnerTab
    {
        public static string[] array = { searchField };
        public static string searchField;
        public static int SelectedObject = 0;
        private static Vector2 ItemsScrollPosition;
        private static Vector2 OptionsScrollPosition;

        public static void Tab()
        {

            var sortedList = G.InteractableButtons.ToList().ToArray();

            GUILayout.Space(0);

            GUILayout.BeginArea(new Rect(10, 35, 260, 400), style: "box", text: "Interactables");

            ItemsScrollPosition = GUILayout.BeginScrollView(ItemsScrollPosition);
            searchField = GUILayout.TextField(searchField);

            if (!String.IsNullOrEmpty(searchField))
            {
                sortedList = Items.FindMatches<GUIContent>(sortedList, x => x.text, searchField);
                SelectedObject = GUILayout.SelectionGrid((int)SelectedObject, sortedList, 1);
            }
            if (String.IsNullOrEmpty(searchField))
                SelectedObject = GUILayout.SelectionGrid((int)SelectedObject, sortedList, 1);

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(280, 35, 260, 400), style: "box", text: "Options");
            OptionsScrollPosition = GUILayout.BeginScrollView(OptionsScrollPosition);

            if (GUILayout.Button("Spawn " + sortedList[SelectedObject].text))
            {
                var position = G.LocalPlayer.GetBody();

                G.SpawnCards[SelectedObject].DoSpawn(position.transform.position + position.inputBank.GetAimRay().direction * 1.6f, new Quaternion(), new DirectorSpawnRequest(
                    G.SpawnCards[SelectedObject],
                    new DirectorPlacementRule
                    {
                        placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
                        maxDistance = 100f,
                        minDistance = 20f,
                        position = position.transform.position + position.inputBank.GetAimRay().direction * 1.6f,
                        preventOverhead = true
                    },
                    RoR2Application.rng)
                );
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}
