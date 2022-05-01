using Aerolt.Cheats;
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
    public class MonsterTab
    {
        public static string[] array = { searchField };
        public static string searchField;
        public static int SelectedObject = 0;
        private static Vector2 MonsterScrollPosition;
        private static Vector2 OptionsScrollPosition;
        public static void Tab()
        {
            var sortedList = G.MonsterButtons.OrderBy(x => x.text).ToList().ToArray();

            GUILayout.Space(0);

            GUILayout.BeginArea(new Rect(10, 35, 260, 400), style: "box", text: "Monsters");
            searchField = GUILayout.TextField(searchField);
            MonsterScrollPosition = GUILayout.BeginScrollView(MonsterScrollPosition);

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

            Vector3 location = G.Localbody.master.GetBody().transform.position;
            var masterprefab = MasterCatalog.FindMasterPrefab(sortedList[SelectedObject].text);
            var body = masterprefab.GetComponent<CharacterMaster>().bodyPrefab;

            if (GUILayout.Button("Spawn"))
            {
                var bodyGameObject = UnityEngine.Object.Instantiate<GameObject>(masterprefab, location, Quaternion.identity);
                CharacterMaster master = bodyGameObject.GetComponent<CharacterMaster>();
                NetworkServer.Spawn(bodyGameObject);
                master.bodyPrefab = body;
                master.SpawnBody(G.Localbody.master.GetBody().transform.position, Quaternion.identity);
            }
               

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    }
}
