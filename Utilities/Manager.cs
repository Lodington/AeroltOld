using Aerolt.Cheats;
using Aerolt.Classes;
using Aerolt.Overrides;
using RoR2;
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
            T.Log("Loading Aerolt");
            AttributeUtilities.LinkAttributes();
            T.Log("Getting Config");
            AssetUtilities.GetAssets();
            T.Log("Getting Assets");
            Colors.AddColors();
            #region Overrides   
            T.Log("Starting Hooks");
            //SceneDirector.onPostPopulateSceneServer += H.GetData;
            CharacterBody.onBodyStartGlobal += H.GetCharacterData;
            SceneDirector.onPostPopulateSceneServer += H.GetESPData;
            SceneManager.sceneLoaded += H.OnSceneLoaded;
            T.Log("Hooks Complete");
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