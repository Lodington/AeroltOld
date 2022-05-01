using Aerolt.Cheats;
using Aerolt.Classes;
using Aerolt.Overrides;
using RoR2;
using RoR2.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Aerolt.Utilities
{

    public class Manager : MonoBehaviour
    {

        void Start()
        {
            T.Log(LogLevel.Information,"Loading Aerolt");
            AttributeUtilities.LinkAttributes();
            T.Log(LogLevel.Information, "Getting Config");
            AssetUtilities.GetAssets();
            T.Log(LogLevel.Information, "Getting Assets");
            Colors.AddColors();
            #region Overrides   
            T.Log(LogLevel.Information, "Starting Hooks");

            CharacterBody.onBodyStartGlobal += H.GetCharacterData;
            SceneDirector.onPostPopulateSceneServer += H.GetData;
            SceneManager.sceneLoaded += H.OnSceneLoaded;
            NetworkManagerSystem.onClientConnectGlobal += H.PlayerListUpdate;
            NetworkManagerSystem.onClientDisconnectGlobal += H.PlayerListUpdate;

            T.Log(LogLevel.Information, "Hooks Complete");
            #endregion
        }

       
        void OnGUI()
        {
            #region Set Camera Once
            if (G.MainCamera == null)
                G.MainCamera = Camera.main;
            #endregion
        }
       
    }
}