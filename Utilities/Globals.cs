using Aerolt.Options;
using RoR2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aerolt
{
    public class G
    {
        // some global variables
        public static Camera MainCamera;
        public static Config Settings = new Config();

        public static CharacterMaster LocalPlayer;
        public static Inventory LocalPlayerInv;
        public static HealthComponent LocalHealth;
        public static SkillLocator LocalSkills;
        public static NetworkUser LocalNetworkUser;
        public static CharacterBody Localbody;
        public static CharacterMotor LocalMotor;

        public static GameObject CursorGrabber;
        public static List<SpawnCard> InteractablesSpawnCards = new List<SpawnCard>();
        public static List<object> AllMonsterPrefabs = new List<object>();

        public static List<GUIContent> buttons2 = new List<GUIContent>();
        public static List<GUIContent> buttons3 = new List<GUIContent>();
        public static List<GUIContent> buttons4 = new List<GUIContent>();
        public static List<GUIContent> buttons5 = new List<GUIContent>();
        public static List<GUIContent> ItemButtons = new List<GUIContent>();
        public static List<GUIContent> EquipmentButtons = new List<GUIContent>();
        public static List<GUIContent> InteractableButtons = new List<GUIContent>();
        public static List<GUIContent> MonsterButtons = new List<GUIContent>();
        public static List<GUIContent> Players = new List<GUIContent>();

        public static bool _CharacterCollected = false;

        public static string CONSOLEOUTPUT;

        public static int count = 0;

    }
}
