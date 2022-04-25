using Aerolt.Attributes;
using Aerolt.Utilities;
using KinematicCharacterController;
using R2API.Utils;
using RoR2;
using RoR2.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aerolt.Cheats
{
    [Comp]
    class Players : MonoBehaviour
    {
        private static int _collidableLayersCached;

        void Update()
        {
            try
            {
                if (G._CharacterCollected)
                {
                    if (G.Settings.PlayerOptions.aimbotEnabled)
                    {
                        AimBot();
                    }
                    if (G.Settings.PlayerOptions.skillToggle)
                        G.LocalSkills.ApplyAmmoPack();
                    if (G.Settings.PlayerOptions.noClipEnabled)
                        DoNoclip();
                    if (G.Settings.PlayerOptions.alwaysSprint)
                        AlwaysSprint();
                    if (G.Settings.PlayerOptions.doMassiveDamage) 
                        Players.SetBaseDamage(999999f);
                    if (G.Settings.PlayerOptions.doDamageModifier)
                        Players.SetBaseDamage(G.Settings.PlayerOptions.damageModifier);
                    if (G.Settings.PlayerOptions.MaxCrit)
                        Players.SetCrit(100);

                    G.Localbody.levelDamage = G.Settings.PlayerOptions.lvlDamage;
                    G.Localbody.levelCrit = G.Settings.PlayerOptions.lvlCrit;

                    G.Localbody.MarkAllStatsDirty();
                    
                }
            } 
            catch (NullReferenceException){}
            
        }
        #region Player Mods
        public static void AimBot()
        {
            if (T.CursorIsVisible())
                return;
            var localUser = LocalUserManager.GetFirstLocalUser();
            var controller = localUser.cachedMasterController;
            if (!controller)
                return;
            var body = controller.master.GetBody();
            if (!body)
                return;
            var inputBank = body.GetComponent<InputBankTest>();
            var aimRay = new Ray(inputBank.aimOrigin, inputBank.aimDirection);
            var bullseyeSearch = new BullseyeSearch();
            var team = body.GetComponent<TeamComponent>();
            bullseyeSearch.teamMaskFilter = TeamMask.all;
            bullseyeSearch.teamMaskFilter.RemoveTeam(team.teamIndex);
            bullseyeSearch.filterByLoS = true;
            bullseyeSearch.searchOrigin = aimRay.origin;
            bullseyeSearch.searchDirection = aimRay.direction;
            bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
            bullseyeSearch.maxDistanceFilter = float.MaxValue;
            bullseyeSearch.maxAngleFilter = 20f;// ;// float.MaxValue;
            bullseyeSearch.RefreshCandidates();
            var hurtBox = bullseyeSearch.GetResults().FirstOrDefault();
            if (hurtBox)
            {
                Vector3 direction = hurtBox.transform.position - aimRay.origin;
                inputBank.aimDirection = direction;
            }
        }
        public static void AlwaysSprint()
        {
            var localUser = RoR2.LocalUserManager.GetFirstLocalUser();
            if (localUser == null || localUser.cachedMasterController == null || localUser.cachedMasterController.master == null) return;
            var controller = localUser.cachedMasterController;
            var body = controller.master.GetBody();
            if (body && !body.isSprinting && !localUser.inputPlayer.GetButton("Sprint"))
                controller.SetFieldValue("sprintInputPressReceived", true);
        }
        public static void DoNoclip()
        {
            try
            {
                if (G.Localbody.characterMotor.useGravity)
                {
                    if (G.Settings.PlayerOptions.noClipEnabled)
                    {
                        if (_collidableLayersCached != 0)
                        {
                            G.Localbody.GetComponent<KinematicCharacterMotor>().CollidableLayers = _collidableLayersCached;
                        }
                    }
                    else
                    {
                        _collidableLayersCached = G.Localbody.GetComponent<KinematicCharacterMotor>().CollidableLayers;
                        G.Localbody.GetComponent<KinematicCharacterMotor>().CollidableLayers = 0;
                    }
                }
                var forwardDirection = G.Localbody.GetComponent<InputBankTest>().moveVector.normalized;
                var aimDirection = G.Localbody.GetComponent<InputBankTest>().aimDirection.normalized;
                var isForward = Vector3.Dot(forwardDirection, aimDirection) > 0f;

                var isSprinting = G.LocalNetworkUser.inputPlayer.GetButton("Sprint");
                var isStrafing = G.LocalNetworkUser.inputPlayer.GetAxis("MoveVertical") != 0f;

                if (isSprinting)
                {
                    G.Localbody.characterMotor.velocity = forwardDirection * 100f;
                    if (isStrafing)
                    {
                        G.Localbody.characterMotor.velocity.y = aimDirection.y * (isForward ? 100f : -100f);
                    }
                }
                else
                {
                    G.Localbody.characterMotor.velocity = forwardDirection * 50;
                    if (isStrafing)
                    {
                        G.Localbody.characterMotor.velocity.y = aimDirection.y * (isForward ? 50 : -50);
                    }
                }
                var inputBank = G.Localbody.GetComponent<InputBankTest>();
                if (inputBank && inputBank.jump.down)
                    G.Localbody.characterMotor.velocity.y = 50f;
            }
            catch (NullReferenceException) { }
        }
        public static void godMode()
        {
            G.Settings.PlayerOptions.godModeEnabled = !G.Settings.PlayerOptions.godModeEnabled;
            var godToggleMethod = typeof(CharacterMaster).GetMethodCached("ToggleGod");
            bool hasNotYetRun = true;
            foreach (var playerInstance in PlayerCharacterMasterController.instances)
            {
                godToggleMethod.Invoke(playerInstance.master, null);
                if (hasNotYetRun)
                {
                    hasNotYetRun = false;
                }
            }
        }
        #endregion
        #region Currency
        public static void GiveLunarCoinsToAll(uint coinsToGive)
        {
            foreach (var networkUser in NetworkUser.readOnlyInstancesList)
                networkUser.AwardLunarCoins(coinsToGive);
        }
        public static void GiveLunarCoins(NetworkUser networkuser, uint coinsToGive)
        {
            networkuser.AwardLunarCoins(coinsToGive);
        }
        public static void GiveMoney(uint moneyToGive)
        {
            G.LocalPlayer.GiveMoney(moneyToGive);
        }
        public static void giveXP(uint xpToGive)
        {
            G.LocalPlayer.GiveExperience(xpToGive);
        }
        #endregion

        public static void KillAllMobs()
        {
            var localUser = LocalUserManager.GetFirstLocalUser();
            var controller = localUser.cachedMasterController;
            if (!controller)
                return;
            
            var body = controller.master.GetBody();
            if (!body)
                return;

            var bullseyeSearch = new BullseyeSearch
            {
                filterByLoS = false,
                maxDistanceFilter = float.MaxValue,
                maxAngleFilter = float.MaxValue
            };

            bullseyeSearch.RefreshCandidates();
            var hurtBoxList = bullseyeSearch.GetResults();
            foreach (var hurtbox in hurtBoxList)
            {
                var mob = HurtBox.FindEntityObject(hurtbox);
                string mobName = mob.name.Replace("Body(Clone)", "");
                if (ContentManager._survivorDefs.Any(x => x.cachedName.Equals(mobName)))
                    continue;
                
                else
                {
                    var health = mob.GetComponent<HealthComponent>();
                    health.Suicide();
                    Chat.AddMessage($"<color=yellow>Killed {mobName} </color>");
                }

            }
        }
        #region Inventory Manipulation

        public static void ClearInventory(Inventory playerInv)
        {
            if (playerInv)
            {
                foreach (var itemDef in ContentManager._itemDefs)
                {

                    playerInv.RemoveItem(itemDef, playerInv.GetItemCount(itemDef));

                }
                playerInv.SetEquipmentIndex(EquipmentIndex.None);
            }
        }

        public static void RollItems(string amount, Inventory playerInv)
        {
            try
            {
                int num;
                TextSerialization.TryParseInvariant(amount, out num);
                if (num > 0)
                {
                    WeightedSelection<List<PickupIndex>> weightedSelection = new WeightedSelection<List<PickupIndex>>(8);
                    weightedSelection.AddChoice(Run.instance.availableTier1DropList, 80f);
                    weightedSelection.AddChoice(Run.instance.availableTier2DropList, 19f);
                    weightedSelection.AddChoice(Run.instance.availableTier3DropList, 1f);
                    for (int i = 0; i < num; i++)
                    {

                        List<PickupIndex> list = weightedSelection.Evaluate(UnityEngine.Random.value);
                        playerInv.GiveItem(list[UnityEngine.Random.Range(0, list.Count)].itemIndex, 1);
                    }
                }
            }
            catch (ArgumentException)
            {
            }
        }
        public static void GiveAllItemsToAll(int amount)
        {
            foreach (var networkUser in NetworkUser.readOnlyInstancesList)
            {
                foreach (var itemDef in ContentManager._itemDefs)
                {
                    //plantonhit kills you when you pick it up
                   // if (itemDef == RoR2Content.Items.PlantOnHit || itemDef == RoR2Content.Items.HealthDecay || itemDef == RoR2Content.Items.TonicAffliction || itemDef == RoR2Content.Items.BurnNearby || itemDef == RoR2Content.Items.CrippleWardOnLevel || itemDef == RoR2Content.Items.Ghost || itemDef == RoR2Content.Items.ExtraLifeConsumed)
                     //   continue;
                    //ResetItem sets quantity to 1, RemoveItem removes the last one.

                    networkUser.master.inventory.GiveItem(itemDef, amount);
                }
            }
        }
        public static void GiveAllItems(Inventory LocalPlayerInv, int amount)
        {
            if (LocalPlayerInv)
            {
                foreach (var itemDef in ContentManager._itemDefs)
                {
                    //plantonhit kills you when you pick it up
                   // if (itemDef == RoR2Content.Items.PlantOnHit || itemDef == RoR2Content.Items.HealthDecay || itemDef == RoR2Content.Items.TonicAffliction || itemDef == RoR2Content.Items.BurnNearby || itemDef == RoR2Content.Items.CrippleWardOnLevel || itemDef == RoR2Content.Items.Ghost || itemDef == RoR2Content.Items.ExtraLifeConsumed)
                   //     continue;
                    //ResetItem sets quantity to 1, RemoveItem removes the last one.
                    LocalPlayerInv.GiveItem(itemDef, amount);
                }
            }
        }
        #endregion

        #region Set Values
        public static void SetCrit(float Crit)
        {
            try
            {
                G.Localbody.baseCrit = Crit;
            }
            catch (NullReferenceException)
            {
            }
        }

        public static void SetBaseDamage(float BaseDamage)
        {
            try
            {
                G.Localbody.baseDamage = BaseDamage;
            }
            catch (NullReferenceException)
            {
            }
        }
        public static void SetplayersAttackSpeed(float attackSpeed)
        {
            try
            {
                G.Localbody.baseAttackSpeed = attackSpeed;
            }
            catch (NullReferenceException)
            {
            }
        }
        public static void SetplayersArmor(float armor)
        {
            try
            {
                G.Localbody.baseArmor = armor;
            }
            catch (NullReferenceException)
            {
            }
        }
        public static void SetplayersMoveSpeed(float movespeed)
        {
            try
            {
                G.Localbody.baseMoveSpeed = movespeed;
            }
            catch (NullReferenceException)
            {
            }
        }
    }
    #endregion
}
