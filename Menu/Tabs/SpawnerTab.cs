using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Aerolt.Menu.Tabs
{
    public class SpawnerTab
    {
        private static Vector2 ItemsScrollPosition;
        public static List<InteractableSpawnCard> interactableSpawnCards = new List<InteractableSpawnCard>();

        public static void Tab()
        {

            GUILayout.Space(0);
            
            GUILayout.BeginArea(new Rect(10, 35, 260, 400), style: "box", text: "Interactables");
            ItemsScrollPosition = GUILayout.BeginScrollView(ItemsScrollPosition);

            foreach (InteractableSpawnCard interactable in interactableSpawnCards)
            {
                GUILayout.Button(interactable.name);
            }


            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }


    }
}
