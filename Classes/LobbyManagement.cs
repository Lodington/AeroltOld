using Aerolt.Menu;
using RoR2;
using System;
using UnityEngine;
using Console = RoR2.Console;

namespace Aerolt.Classes
{
    class LM
    {
        public static NetworkUser GetNetUserFromString(string playerString)
        {
            if (playerString != "")
            {
                if (int.TryParse(playerString, out int result))
                {
                    if (result < NetworkUser.readOnlyInstancesList.Count && result >= 0)
                    {
                        return NetworkUser.readOnlyInstancesList[result];
                    }
                    return null;
                }
                else
                {
                    foreach (NetworkUser n in NetworkUser.readOnlyInstancesList)
                    {
                        if (n.userName.Equals(playerString, StringComparison.CurrentCultureIgnoreCase))
                        {
                            return n;
                        }
                    }
                    return null;
                }
            }
            return null;
        }
        public static void GetPlayers()
        {

            G.Players.Clear();
            NetworkUser n;

            for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
            {
                n = NetworkUser.readOnlyInstancesList[i];
                G.Players.Add(new GUIContent(n.userName));
            }
            
        }
        public static void kickPlayer(NetworkUser PlayerName, NetworkUser LocalNetworkUser) { Console.instance.RunClientCmd(LocalNetworkUser, "kick_steam", new string[] { PlayerName.Network_id.steamId.ToString() }); }
        public static void banPlayer(NetworkUser PlayerName, NetworkUser LocalNetworkUser) { Console.instance.RunClientCmd(LocalNetworkUser, "ban_steam", new string[] { PlayerName.Network_id.steamId.ToString() }); }

    }
}
