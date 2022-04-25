﻿using R2API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Aerolt.Utilities
{
    public class AssetUtilities
    {
        public static Dictionary<string, Shader> Shaders = new Dictionary<string, Shader>();
        public static GUISkin Skin;
        public static GUISkin VanillaSkin;
        public static AudioClip BonkClip;
        public static void GetAssets()
        {

            AssetBundle Bundle = AssetBundle.LoadFromMemory(Properties.Resources.GuiSkin);
           
            foreach (Shader s in Bundle.LoadAllAssets<Shader>())
                Shaders.Add(s.name, s);
            BonkClip = Bundle.LoadAllAssets<AudioClip>().First();
            VanillaSkin = Bundle.LoadAllAssets<GUISkin>().First();
            if (!String.IsNullOrEmpty(G.Settings.MiscOptions.UISkin))
                LoadGUISkinFromName(G.Settings.MiscOptions.UISkin);
            else
                Skin = VanillaSkin;
        }
        public static void LoadGUISkinFromName(string name)
        {
            if (File.Exists(Application.dataPath + "/GUISkins/" + name + ".assets"))
            {
                AssetBundle tempAsset = AssetBundle.LoadFromMemory(File.ReadAllBytes(Application.dataPath + "/GUISkins/" + name + ".assets"));
                Skin = tempAsset.LoadAllAssets<GUISkin>().First();
                tempAsset.Unload(false);
                G.Settings.MiscOptions.UISkin = name;
            }
            else
            {
                Skin = VanillaSkin;
                G.Settings.MiscOptions.UISkin = "";
            }
        }
        public static List<string> GetSkins(bool Extensions = false)
        {
            //just kinda reads from the guiskins folder
            List<string> files = new List<string>();
            DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/GUISkins/");
            FileInfo[] Files = d.GetFiles("*.assets");
            foreach (FileInfo file in Files)
            {
                if (Extensions)
                    files.Add(file.Name.Substring(0, file.Name.Length));
                else
                    files.Add(file.Name.Substring(0, file.Name.Length - 7)); // - 7 removes .assets
            }
            return files;
        }
       
    }
}
