using Aerolt.Attributes;
using Aerolt.Classes;
using Aerolt.Menu.Tabs;
using Aerolt.Menu.Windows;
using RoR2;
using RoR2.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using AssetUtilities = Aerolt.Utilities.AssetUtilities;

namespace Aerolt.Menu
{
    [Comp]
    public class Main : MonoBehaviour
    {
        public static MenuTab SelectedTab = MenuTab.Items;
        public static bool MenuOpen = false;
        public static string DropdownTitle;
        public static Rect DropdownPos;
        public static Color GUIColor;

        private List<GUIContent> buttons = new List<GUIContent>();

        public static Rect CursorPos = new Rect(0, 0, 20f, 20f);

        public static Rect windowRect2 = new Rect(10, 35, 260, 400);

        private int i = -40;
        private Texture _cursorTexture;
        public static Rect windowRect = new Rect(80, 80, 550, 450);
        private Rect itemRect = new Rect(400, 465, 200, 250);
        private Rect guiRect = new Rect(0, Screen.height-65, 550, 700);
        public static string command;
        readonly string Name = "Aerolt";
        readonly string Version = "v1.3.0";

        void Start()
        {
            GUIColor = GUI.color;
            foreach (MenuTab val in Enum.GetValues(typeof(MenuTab)))
                buttons.Add(new GUIContent(Enum.GetName(typeof(MenuTab), val)));
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (MenuOpen == false)
                {
                    MenuOpen = true;
                    G.CursorGrabber.SetActive(MenuOpen);
                }
                else if (MenuOpen == true)
                {
                    MenuOpen = false;
                    G.CursorGrabber.SetActive(MenuOpen);
                    
                    i = -40;
                }
            }
        }
        void OnGUI()
        {
            if (MenuOpen)
            {
                GUI.skin = AssetUtilities.Skin;

                if (_cursorTexture == null)
                    _cursorTexture = Resources.Load("textures/miscicons/texCursor") as Texture;

                GUI.depth = -1;

                GUIStyle guiStyle = new GUIStyle("label");
                guiStyle.margin = new RectOffset(10, 10, 5, 5);
                guiStyle.fontSize = 22;
                if (i < 0)
                    i++;

                windowRect = GUILayout.Window(0, windowRect, MenuWindow, Enum.GetName(typeof(MenuTab), SelectedTab));
                guiRect = GUILayout.Window(9, this.guiRect, GUIWindow.Window, "Console");

                using (var topbar = new GUILayout.AreaScope(new Rect(0, i, Screen.width, 40), "", "NavBox"))
                {
                    using (var horizontalScope = new GUILayout.HorizontalScope())
                    {
                        GUI.color = new Color32(34, 177, 76, 255);
                        GUILayout.Label($"<b>{Name}</b> <size=15>{Version}</size>", guiStyle);
                        GUI.color = GUIColor;
                        SelectedTab = (MenuTab)GUILayout.Toolbar((int)SelectedTab, buttons.ToArray(), style: "TabBtn");
                    }
                }

               using (var lowerbar = new GUILayout.AreaScope(new Rect(0, Screen.height - 40, Screen.width, 40), "", "NavBox"))
               {
                    using (var horizontalScope = new GUILayout.HorizontalScope())
                    {
                        GUI.color = new Color32(34, 177, 76, 255);
                        GUI.color = GUIColor;
                        GUILayout.Label("If you have any issues please feel free to join the discord! 1.3.0 is a huge update, so please look around for all of the options.", guiStyle);
                        if (GUILayout.Button("Join Discord"))
                            System.Diagnostics.Process.Start("https://discord.gg/X4GHM9fePg");
                        if (GUILayout.Button("Buy me a coffee!"))
                            System.Diagnostics.Process.Start("https://www.buymeacoffee.com/lodington");
                    }   
                }
               
                GUI.depth = -2;
                CursorPos.x = Input.mousePosition.x;
                CursorPos.y = Screen.height - Input.mousePosition.y;

                GUI.DrawTexture(CursorPos, _cursorTexture);
                Cursor.lockState = CursorLockMode.None;

                GUI.skin = null;
            }
        }

        

        void MenuWindow(int windowID)
        {
            #region Display Selected Tab
            switch (SelectedTab)
            {
                case MenuTab.Items:
                    ItemsTab.Tab();
                    break;
                case MenuTab.Equipment:
                    EquipmentTab.Tab();
                    break;
                case MenuTab.ESP:
                    EspTab.Tab();
                    break;
                case MenuTab.Teleporter:
                    TeleporterTab.Tab();
                    break;
                case MenuTab.Lobby:
                    LobbyTab.Tab();
                    break;
                case MenuTab.Player:
                    PlayersTab.Tab();
                    break;
                case MenuTab.Interactable:
                    InteractableTab.Tab();
                    break;
                case MenuTab.Monsters:
                    MonsterTab.Tab();
                    break;
            }
            #endregion

            GUI.DragWindow();
        }
    }
}
