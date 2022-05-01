using Aerolt.Classes;
using Aerolt.Utilities;
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
            GUILayout.Space(0);
            GUILayout.BeginArea(new Rect(10, 35, 260, 400), style: "box", text: "Player");
            LobbyScrollPosition = GUILayout.BeginScrollView(LobbyScrollPosition);

            SelectedObject = GUILayout.SelectionGrid((int)SelectedObject, G.Players.ToArray(), 1);

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            GUILayout.Space(0);

            GUILayout.BeginArea(new Rect(280, 35, 260, 400), style: "box", text: "Options");

            if (G.Players.Any())
            {
                NetworkUser player = LM.GetNetUserFromString(G.Players[SelectedObject].text);
                CharacterMaster master = player.master;

                if (GUILayout.Button("Kick " + G.Players[SelectedObject].text))
                {
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage { baseToken = "<color=red>Kicked Player " + G.Players[SelectedObject].text + "</color>" });

                    LM.kickPlayer(LM.GetNetUserFromString(G.Players[SelectedObject].text), G.LocalNetworkUser);

                    T.Log(LogLevel.Information, "Kicked Player " + G.Players[SelectedObject].text);
                }
                if (GUILayout.Button("Ban " + G.Players[SelectedObject].text))
                {
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage { baseToken = "<color=red>Banned Player " + G.Players[SelectedObject].text + "</color>" });

                    LM.banPlayer(LM.GetNetUserFromString(G.Players[SelectedObject].text), G.LocalNetworkUser);
                    T.Log(LogLevel.Information, "Banned Player " + G.Players[SelectedObject].text);
                }
                if (GUILayout.Button("Resurrect " + G.Players[SelectedObject].text))
                {
                    Chat.SendBroadcastChat(new Chat.SimpleChatMessage { baseToken = "<color=yellow>Resurrected " + G.Players[SelectedObject].text + "</color>" });

                    Transform spawnPoint = Stage.instance.GetPlayerSpawnTransform();
                    master.Respawn(spawnPoint.position, spawnPoint.rotation);
                    T.Log(LogLevel.Information, "Respawned Player " + G.Players[SelectedObject].text);
                }
                if (GUILayout.Button("Kill " + G.Players[SelectedObject].text))
                {
                    master.TrueKill();

                    T.Log(LogLevel.Information, "Killed Player " + G.Players[SelectedObject].text);
                }
            }
            GUILayout.EndArea();
        }
    }
}
