using UnityEngine;

namespace Aerolt.Menu.Windows
{
    public class WhitelistWindow
    {
        public static bool WhitelistMenuOpen = false;
        public static void Window(int windowID)
        {



            GUILayout.Space(3);
            GUILayout.BeginVertical(style: "SelectedButtonDropdown");

            GUILayout.EndVertical();

            if (GUILayout.Button("Close Window"))
                WhitelistMenuOpen = !WhitelistMenuOpen;
            GUI.DragWindow();
        }
    }
}
