using Aerolt.Classes;
using R2API.Utils;
using RoR2;
using System.Linq;
using UnityEngine;

namespace Aerolt.Menu.Tabs
{
    public class LobbyTab
    {
        public static int SelectedObject = 0;
        private static Vector2 LobbyScrollPosition;
        public static void Tab()
        {
           
            GUILayout.BeginArea(new Rect(10, 35, 260, 400), style: "box", text: "Player");
            LobbyScrollPosition = GUILayout.BeginScrollView(LobbyScrollPosition);

            SelectedObject = GUILayout.SelectionGrid((int)SelectedObject, Main.Players.ToArray(), 1);
            foreach(var player in Main.Players)
            {
                GUILayout.Button(player.text);
                    
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(280, 35, 260, 400), style: "box", text: "Options");

            if (Main.Players.Any())
            {
                NetworkUser player = LM.GetNetUserFromString(Main.Players[SelectedObject].text);
                CharacterMaster master = player.master;
                if (GUILayout.Button("Kick " + Main.Players[SelectedObject].text))
                {
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage { baseToken = "<color=red>Kicked Player " + Main.Players[SelectedObject].text + "</color>" });

                    LM.kickPlayer(LM.GetNetUserFromString(Main.Players[SelectedObject].text), G.LocalNetworkUser);
                }
                if (GUILayout.Button("Ban " + Main.Players[SelectedObject].text))
                {
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage { baseToken = "<color=red>Banned Player " + Main.Players[SelectedObject].text + "</color>" });

                    LM.banPlayer(LM.GetNetUserFromString(Main.Players[SelectedObject].text), G.LocalNetworkUser);
                }
                if (GUILayout.Button("Resurrect " + Main.Players[SelectedObject].text))
                {
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage { baseToken = "<color=yellow>Resurrected " + Main.Players[SelectedObject].text + "</color>" });
                    master.Respawn(master.GetFieldValue<Vector3>("deathFootPosition"), Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f));
                    master.GetBody().AddTimedBuff(RoR2Content.Buffs.Immune, 3f);
                }
                if (GUILayout.Button("Kill " + Main.Players[SelectedObject].text))
                {
                    master.TrueKill();
                }
            }
            GUILayout.EndArea();
        }
    }
}
