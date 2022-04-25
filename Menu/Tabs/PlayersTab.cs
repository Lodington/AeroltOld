using Aerolt.Cheats;
using Aerolt.Classes;
using UnityEngine;

namespace Aerolt.Menu.Tabs
{
    public class PlayersTab
    {

        private static Vector2 scrollPosition1 = new Vector2(0, 0);
        public static void Tab()
        {
            GUILayout.Space(0);
            GUILayout.BeginArea(new Rect(10, 35, 260, 400), style: "box", text: "Player Values");
            scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1/*, GUILayout.Width(480)*/);
            
            GUILayout.Label("Toggles : ");

            if (GUILayout.Button("Toggle God Mode : " + G.Settings.PlayerOptions.godModeEnabled))
                Players.godMode();

            G.Settings.PlayerOptions.skillToggle = GUILayout.Toggle(G.Settings.PlayerOptions.skillToggle, "Infinite Skills : " + G.Settings.PlayerOptions.skillToggle);
            G.Settings.PlayerOptions.noClipEnabled = GUILayout.Toggle(G.Settings.PlayerOptions.noClipEnabled, "No Clip : " + G.Settings.PlayerOptions.noClipEnabled);
            G.Settings.PlayerOptions.alwaysSprint = GUILayout.Toggle(G.Settings.PlayerOptions.alwaysSprint, "Always Sprint : " + G.Settings.PlayerOptions.alwaysSprint);
            G.Settings.PlayerOptions.aimbotEnabled = GUILayout.Toggle(G.Settings.PlayerOptions.aimbotEnabled, "Aimbot Enabled : " + G.Settings.PlayerOptions.aimbotEnabled);

            GUILayout.Space(20);

            GUILayout.Label("Player Values : ");
            G.Settings.PlayerOptions.doMassiveDamage = GUILayout.Toggle(G.Settings.PlayerOptions.doMassiveDamage, "Do Massive Damage : " + G.Settings.PlayerOptions.doMassiveDamage);
            G.Settings.PlayerOptions.MaxCrit = GUILayout.Toggle(G.Settings.PlayerOptions.MaxCrit, "Max Crit : " + G.Settings.PlayerOptions.MaxCrit);
            GUILayout.Label("Damage Per lvl : " + G.Settings.PlayerOptions.lvlDamage);
            G.Settings.PlayerOptions.lvlDamage = (int)GUILayout.HorizontalSlider(G.Settings.PlayerOptions.lvlDamage, 1, 100);
            GUILayout.Label("Crit Per lvl : " + G.Settings.PlayerOptions.lvlCrit);
            G.Settings.PlayerOptions.lvlCrit = (int)GUILayout.HorizontalSlider(G.Settings.PlayerOptions.lvlCrit, 1, 100);

            GUILayout.Space(20);

            GUILayout.Label("Items :");

            GUILayout.Label("Amount per Item : " + G.Settings.PlayerOptions.amountOfItemsToGive);
            G.Settings.PlayerOptions.amountOfItemsToGive = (int)GUILayout.HorizontalSlider(G.Settings.PlayerOptions.amountOfItemsToGive, 1, 100);
            if (GUILayout.Button("Give all items x" + G.Settings.PlayerOptions.amountOfItemsToGive))
                Players.GiveAllItems(G.LocalPlayerInv, G.Settings.PlayerOptions.amountOfItemsToGive);
            if (GUILayout.Button("Give all items to everyone x" + G.Settings.PlayerOptions.amountOfItemsToGive))
                Players.GiveAllItemsToAll(G.Settings.PlayerOptions.amountOfItemsToGive);
            if (GUILayout.Button("Roll "+ G.Settings.PlayerOptions.amountOfItemsToGive + " items"))
                Players.RollItems(G.Settings.PlayerOptions.amountOfItemsToGive.ToString(), G.LocalPlayerInv);

            if (GUILayout.Button("Clear Inventory"))
                Players.ClearInventory(G.LocalPlayerInv);

            if (GUILayout.Button("Kill All Mobs"))
                Players.KillAllMobs();

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(280, 35, 260, 400), style: "box", text: "Currency");
            GUILayout.Label("Money To Give : " + G.Settings.PlayerOptions.moneyToGive);
            G.Settings.PlayerOptions.moneyToGive = (uint)GUILayout.HorizontalSlider(G.Settings.PlayerOptions.moneyToGive, 1, 10000);
            if (GUILayout.Button("Give Money"))
                Players.GiveMoney(G.Settings.PlayerOptions.moneyToGive);

            GUILayout.Label("EXP To Give : " + G.Settings.PlayerOptions.xpToGive);
            G.Settings.PlayerOptions.xpToGive = (uint)GUILayout.HorizontalSlider(G.Settings.PlayerOptions.xpToGive, 1, 10000);
            if (GUILayout.Button("Give EXP"))
                Players.giveXP(G.Settings.PlayerOptions.xpToGive);

            GUILayout.Space(20);

            GUILayout.Label("Lunar Coins To Give Self : " + G.Settings.PlayerOptions._LunarCoinsSelf);
            G.Settings.PlayerOptions._LunarCoinsSelf = (uint)GUILayout.HorizontalSlider(G.Settings.PlayerOptions._LunarCoinsSelf, 1, 10000);
            if (GUILayout.Button("Give Lunar To Self"))
                Players.GiveLunarCoins(G.LocalNetworkUser, G.Settings.PlayerOptions._LunarCoinsSelf);

            GUILayout.Label("Lunar Coins To Give All : " + G.Settings.PlayerOptions._LunarCoinsAll);
            G.Settings.PlayerOptions._LunarCoinsAll = (uint)GUILayout.HorizontalSlider(G.Settings.PlayerOptions._LunarCoinsAll, 1, 10000000);
            if (GUILayout.Button("Give Lunar To All"))
                Players.GiveLunarCoinsToAll(G.Settings.PlayerOptions._LunarCoinsAll);

            GUILayout.EndArea();
        }
    }
}
