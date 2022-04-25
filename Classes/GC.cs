using Aerolt;
using RoR2;
using System;
using UnityEngine;

namespace Aerolt.Classes
{
    class GC : MonoBehaviour
    {
        private static bool firstRun = false;

        public static void getCharacter()
        {
            try
            {
                G.LocalNetworkUser = null;
                
                foreach (NetworkUser networkUser in NetworkUser.readOnlyInstancesList)
                {
                    if (networkUser.isLocalPlayer)
                    {
                        G.LocalNetworkUser = networkUser;
                        G.LocalPlayer = G.LocalNetworkUser.master;
                        G.LocalPlayerInv = G.LocalPlayer.GetComponent<Inventory>();
                        G.LocalHealth = G.LocalPlayer.GetBody().GetComponent<HealthComponent>();
                        G.LocalSkills = G.LocalPlayer.GetBody().GetComponent<SkillLocator>();
                        G.Localbody = G.LocalPlayer.GetBody().GetComponent<CharacterBody>();
                        G.LocalMotor = G.LocalPlayer.GetBody().GetComponent<CharacterMotor>();

                        if (!firstRun && G._CharacterCollected)
                        {
                            firstRun = true;
                            G.Settings.PlayerOptions.lvlDamage = G.Localbody.levelDamage;
                            G.Settings.PlayerOptions.lvlCrit = G.Localbody.levelCrit;
                        }


                        if (!G.LocalPlayer.IsDeadAndOutOfLivesServer())
                            G._CharacterCollected = true;
                        else
                            G._CharacterCollected = false;
                    }
                }

            }
            catch (NullReferenceException)
            {
                G._CharacterCollected = false;
            }
        }
    }
}
