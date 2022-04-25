using Aerolt.Options;
using RoR2;
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

        public static bool _CharacterCollected = false;

    }
}
