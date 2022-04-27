using Aerolt.Overrides;
using Aerolt.Utilities;
using BepInEx;
using HarmonyLib;
using RoR2.UI;
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
        var harm = new Harmony(Info.Metadata.GUID);
        new PatchClassProcessor(harm, typeof(H)).Patch();
        CO.AddComponent<Manager>();
        
    }
}
