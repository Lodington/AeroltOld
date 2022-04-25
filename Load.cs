﻿using Aerolt.Utilities;
using BepInEx;
using UnityEngine;

[BepInDependency("com.bepis.r2api")]
[BepInPlugin(GUID, NAME, VERSION)]
public class Load : BaseUnityPlugin
{
    public const string NAME = "Aerolt";
    public const string GUID = "com.Lodington." + NAME;
    public const string VERSION = "1.2.3";

    public static GameObject CO;
    public void Awake()
    {
        //create new gameobject
        CO = new GameObject();
        UnityEngine.Object.DontDestroyOnLoad(CO);
        //let manager use the unity functions
        CO.AddComponent<Manager>();
    }
}
