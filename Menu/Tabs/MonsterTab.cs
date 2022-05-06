using Aerolt.Cheats;
using Aerolt.Classes;
using Aerolt.Utilities;
using RoR2;
using RoR2.CharacterAI;
using System;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.GUILayout;

namespace Aerolt.Menu.Tabs
{
    public class MonsterTab
    {
        public static string[] array = { searchField };
        public static string searchField;
        public static int SelectedObject = 0;
        private static Vector2 MonsterScrollPosition;
        private static Vector2 OptionsScrollPosition;

        public static string SelectedColorIdentifier = "";

        public static void Tab()
        {
            try
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

                GenerateButton();

                foreach (var elite in EliteCatalog.eliteDefs)
                {
                    GenerateButton(elite.ToString());
                }

                GUILayout.EndScrollView();
                GUILayout.EndArea();

                void GenerateButton(string EliteType = "")
                {
                    bool isElite = true;
                    if (String.IsNullOrEmpty(EliteType))
                    {
                        isElite = false;
                    }
                    if (GUILayout.Button($"spawn {EliteType}"))
                        SpawnAI(sortedList[SelectedObject].text, isElite, EliteType, true);
                }
            }

            catch (IndexOutOfRangeException ex)
            {
                searchField = null;
                T.Log(LogLevel.Error, ex);
            }

        }

        private static void SpawnAI(string prefab, bool isElite, string EliteType, bool hasAI)
        {
            
            Vector3 location = G.Localbody.master.GetBody().transform.position;
            var masterprefab = MasterCatalog.FindMasterPrefab(prefab);
            var body = masterprefab.GetComponent<CharacterMaster>().bodyPrefab;

            var bodyGameObject = UnityEngine.Object.Instantiate<GameObject>(masterprefab, location, Quaternion.identity);
            CharacterMaster master = bodyGameObject.GetComponent<CharacterMaster>();

            if (isElite)
                GiveAspect(master, EliteType);
            if(!hasAI)
                UnityEngine.Object.Destroy(master.GetComponent<BaseAI>());

            NetworkServer.Spawn(bodyGameObject);
            master.bodyPrefab = body;
            master.SpawnBody(G.Localbody.master.GetBody().transform.position, Quaternion.identity);
        }

        private static void GiveAspect(CharacterMaster master, string Aspect)
        {
            var eliteDef = int.TryParse(Aspect, out var eliteIndex) ?
                      EliteCatalog.GetEliteDef((EliteIndex)eliteIndex) :
                      EliteCatalog.eliteDefs.FirstOrDefault(d => d.name.ToLower().Contains(Aspect.ToLower(CultureInfo.InvariantCulture)));
            if (eliteDef)
            {
                master.inventory.SetEquipmentIndex(eliteDef.eliteEquipmentDef.equipmentIndex);
                master.inventory.GiveItem(RoR2Content.Items.BoostHp, Mathf.RoundToInt((eliteDef.healthBoostCoefficient - 1) * 10));
                master.inventory.GiveItem(RoR2Content.Items.BoostDamage, Mathf.RoundToInt(eliteDef.damageBoostCoefficient - 1) * 10);
            }
        }
    }
}
