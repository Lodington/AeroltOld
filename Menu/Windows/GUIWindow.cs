using Aerolt.Utilities;
using RoR2;
using UnityEngine;

namespace Aerolt.Menu.Windows
{
    public class GUIWindow
    {
        private static Vector2 scrollPosition;
        static string command;
        private static string Output;

        public static void Window(int windowID)
        {
            GUILayout.Space(0);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            Output = GUILayout.TextArea(Output);

            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();

            command = GUILayout.TextField(command);
            if (GUILayout.Button("Send" ,GUILayout.Width(60)))
            {
                if (string.IsNullOrEmpty(command))
                    return;
                RoR2.Console.instance.SubmitCmd(LocalUserManager.GetFirstLocalUser(), command, true);
                command = "";
            }

            GUILayout.EndHorizontal();

            
            GUI.DragWindow();
        }

        public static void OnLogReceived(RoR2.Console.Log log)
        {
            Output += $"\n[<color=#ffffff>{log.logType}] {log.message}</color>";
            scrollPosition.y = Mathf.Infinity;
        }
    }
}
