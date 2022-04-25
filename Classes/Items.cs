using Aerolt;
using Aerolt.Attributes;
using Aerolt.Menu;
using GC = Aerolt.Classes.GC;
using RoR2;
using RoR2.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Aerolt.Cheats
{
    class Items : MonoBehaviour
    {

        public static void getItemNames()
        {
            if (Main.ItemButtons.ToArray().Length > 0)
                Main.ItemButtons.Clear();
            if (Main.ItemButtons.ToArray().Length <= 0)
            {
                foreach (var def in ContentManager._itemDefs)
                {
                    if (def == null || def.nameToken == null || def.name == null) continue;
                    Main.ItemButtons.Add(new GUIContent(Language.GetString(def.nameToken)));
                }
            }
        }
        public static void getEquipmentNames()
        {
            if (Main.EquipmentButtons.ToArray().Length > 0)
                Main.EquipmentButtons.Clear();
            if (Main.EquipmentButtons.ToArray().Length <= 0)
            {
                foreach (var def in ContentManager._equipmentDefs)
                {
                    if (def == null || def.nameToken == null) continue;
                    Main.EquipmentButtons.Add(new GUIContent(Language.GetString(def.nameToken)));
                }
            }
        }
        //Made by XoXFaby#1337 "Generics"
        public static T[] FindMatches<T>(T[] toMatch, Func<T, string> toString, string filter)
        {
            Regex filterRegex = new Regex(filter, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            List<T> matches = new List<T>();
            foreach (T obj in toMatch)
            {
                if (filterRegex.IsMatch(toString(obj))) matches.Add(obj);
            }
            return matches.ToArray();
        }
        public static ItemDef getItemDef(string SelectedObj)
        {
            foreach (var def in ContentManager._itemDefs)
            {
                if (def == null || def.nameToken == null) continue;
                if (Language.GetString(def.nameToken) == SelectedObj)
                {
                    return def;
                }
            }
            return null;
        }
        public static EquipmentDef getEquipmentDef(string SelectedObj)
        {
            foreach (var def in ContentManager._equipmentDefs)
            {
                if (def == null || def.nameToken == null) continue;
                if (Language.GetString(def.nameToken) == SelectedObj)
                {
                    return def;
                }
            }
            return null;
        }

        public static void removeItem(Inventory playerInv, ItemDef itemdef, int amount)
        {
            if (playerInv)
                if(playerInv.GetItemCount(itemdef) > 0 && playerInv.GetItemCount(itemdef) >= amount)
                    playerInv.RemoveItem(itemdef, amount);
            
        }

        public static void dropItem(ItemDef def, int amount)
        {
            for (int i = 0; i <= amount; i++)
            {
                var localUser = LocalUserManager.GetFirstLocalUser();
                if (localUser.cachedMasterController && localUser.cachedMasterController.master)
                {
                    var body = localUser.cachedMasterController.master.GetBody();
                    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(def.itemIndex), body.transform.position + (Vector3.up * 1.5f), Vector3.up * 20f + body.transform.forward * 2f);
                }
            }
        }

        public static void dropEquipment(EquipmentDef def, int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                var localUser = LocalUserManager.GetFirstLocalUser();
                if (localUser.cachedMasterController && localUser.cachedMasterController.master)
                {
                    var body = localUser.cachedMasterController.master.GetBody();
                    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(def.equipmentIndex), body.transform.position + (Vector3.up * 1.5f), Vector3.up * 20f + body.transform.forward * 2f);
                }
            }

        }
        public static void giveItem(ItemDef def, int amount)
        {
            G.LocalPlayerInv.GiveItem(def, amount);
        }
        public static void GiveEquipment(string def)
        {
            G.LocalPlayerInv.GiveEquipmentString(def);
        }
    }
}
