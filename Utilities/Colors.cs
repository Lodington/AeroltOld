using RoR2;
using UnityEngine;

namespace Aerolt.Utilities
{
    public class Colors
    {
        public static void AddColors()
        {
            AddColor("Chest", Color.red);
            AddColor("Secret_Plates", Color.cyan);
            AddColor("Barrels", new Color32(255, 128, 0, 255));
            AddColor("Scrappers", Color.blue);
        }
        public static string GenerateColoredString(string text, Color color)
        {
            return "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + text + "</color>";
        }
        public static Color32 GetColor(string identifier)
        {
            if (G.Settings.GlobalOptions.GlobalColors.TryGetValue(identifier, out var toret))
                return toret;

            return Color.magenta;
        }

        public static void AddColor(string id, Color32 c)
        {
            if (!G.Settings.GlobalOptions.GlobalColors.ContainsKey(id))
                G.Settings.GlobalOptions.GlobalColors.Add(id, c);
        }

        public static void SetColor(string id, Color32 c) => G.Settings.GlobalOptions.GlobalColors[id] = c;

        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }
    }
}
