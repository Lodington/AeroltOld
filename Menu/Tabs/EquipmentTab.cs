using Aerolt.Cheats;
using Aerolt.Classes;
using Aerolt.Utilities;
using System;
using System.Linq;
using UnityEngine;

namespace Aerolt.Menu.Tabs
{
    public class EquipmentTab
    {
        public static string[] array = { searchField };
        public static string searchField;
        public static int SelectedObject = 0;
        private static Vector2 ItemsScrollPosition;
        private static Vector2 OptionsScrollPosition;
        public static void Tab()
        {
            try
            {

                var sortedList = G.EquipmentButtons.OrderBy(x => x.text).ToList().ToArray();

                GUILayout.Space(0);

                GUILayout.BeginArea(new Rect(10, 35, 260, 400), style: "box", text: "Equipment");
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
                    Items.GiveEquipment((sortedList[SelectedObject].text));
                if (GUILayout.Button("Drop x1"))
                    Items.dropEquipment(Items.getEquipmentDef((sortedList[SelectedObject].text)), 1);

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
