using Aerolt.Cheats;
using Aerolt.Utilities;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
namespace Aerolt.Menu.Tabs
{
    public class DevTab
    {
        private static Vector2 scrollPosition;
        public static void Tab()
        {
            GUILayout.Space(0);
            GUILayout.BeginArea(new Rect(10, 35, 530, 400), style: "box", text: "ESP Options");
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            if(GUILayout.Toggle(false,"Stop Sound"))
            {
                AkSoundEngine.StopAll();
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
            
           
}
}
