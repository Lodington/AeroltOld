using Aerolt.Cheats;
using Aerolt.Classes;
using Aerolt.Utilities;
using RoR2;
using System;
using System.Linq;
using UnityEngine;
using GC = Aerolt.Classes.GC;

namespace Aerolt.Menu.Tabs
{
    public class ItemsTab
    {
        public static int SelectedObject = 0;
        private static Vector2 ItemsScrollPosition;
        private static Vector2 OptionsScrollPosition;
        public static string searchField;
        public static string[] array = { searchField };
        public static Inventory networkUserInventory;

        public static void Tab()
        {
            try
            {
                var sortedList = G.ItemButtons.OrderBy(x => x.text).ToList().ToArray();

                GUILayout.Space(0);

                GUILayout.BeginArea(new Rect(10, 35, 260, 400), style: "box", text: "Items");
                searchField = GUILayout.TextField(searchField);
                ItemsScrollPosition = GUILayout.BeginScrollView(ItemsScrollPosition);


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

                if (GUILayout.Button("Give Self x1"))
                    Items.giveItem(Items.getItemDef(sortedList[SelectedObject].text), 1);
                if (GUILayout.Button("Give Self x10"))
                    Items.giveItem(Items.getItemDef(sortedList[SelectedObject].text), 10);
                if (GUILayout.Button("Drop x1"))
                    Items.dropItem(Items.getItemDef(sortedList[SelectedObject].text), 1);
                if (GUILayout.Button("Drop x10"))
                    Items.dropItem(Items.getItemDef(sortedList[SelectedObject].text), 10);

                GUILayout.Space(10);

                if (GUILayout.Button("Remove x1"))
                    Items.removeItem(G.LocalPlayerInv, Items.getItemDef(sortedList[SelectedObject].text), 1);
                if (GUILayout.Button("Remove x5"))
                    Items.removeItem(G.LocalPlayerInv, Items.getItemDef(sortedList[SelectedObject].text), 5);

                GUILayout.Space(10);

                foreach (var player in G.Players)
                {
                    if (GUILayout.Button("Remove From " + player.text))
                    {
                        NetworkUser networkUser = LM.GetNetUserFromString(player.text);
                        networkUserInventory = networkUser.master.GetComponent<Inventory>();
                        Items.removeItem(networkUserInventory, Items.getItemDef(sortedList[SelectedObject].text), 1);
                    }
                }

                GUILayout.EndScrollView();
                GUILayout.EndArea();
            }
            catch (IndexOutOfRangeException ex)
            {
                searchField = null;
                T.Log(LogLevel.Error, ex);
            }
        }
    }
}
