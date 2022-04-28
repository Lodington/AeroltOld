using Aerolt.Classes;

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = System.Random;

namespace Aerolt.Utilities
{
    public class T
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool GetCurrentHwProfile(IntPtr fProfile);

        [StructLayout(LayoutKind.Sequential)]
        class HWProfile
        {
            public Int32 dwDockInfo;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 39)]
            public string szHwProfileGuid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szHwProfileName;
        }

        public static Vector2 DropdownCursorPos;
        public static float LastMovementCheck;
        public static LineRenderer LineRenderer;
        public static Material DrawMaterial;
        private static readonly Texture2D backgroundTexture = Texture2D.whiteTexture;
        private static readonly GUIStyle textureStyle = new GUIStyle { normal = new GUIStyleState { background = backgroundTexture } };

        public static bool InScreenView(Vector3 scrnpt)
        {
            if (scrnpt.z <= 0f || scrnpt.x <= 0f || scrnpt.x >= 1f || scrnpt.y <= 0f || scrnpt.y >= 1f)
                return false;

            return true;
        }

        public static float GetDistance(Vector3 endpos)
        {
            return (float)System.Math.Round(Vector3.Distance(Camera.main.transform.position, endpos));
        }

        public static void DrawSnapline(Vector3 worldpos, Color color)
        {
            LineRenderer.startColor = Color.red;
            LineRenderer.endColor = Color.red;
            LineRenderer.startWidth = 0.3f;
            LineRenderer.endWidth = 0.3f;
            LineRenderer.SetPosition(0, Camera.main.transform.position);
            LineRenderer.SetPosition(1, worldpos);
        }
        public static Boolean CursorIsVisible()
        {
            foreach (var mpeventSystem in RoR2.UI.MPEventSystem.readOnlyInstancesList)
                if (mpeventSystem.isCursorVisible)
                    return true;
            return false;
        }
        public static void DrawESPLabel(Vector3 worldpos, Color textcolor, Color outlinecolor, string text, string outlinetext = null)
        {
            GUIContent content = new GUIContent(text);
            if (outlinetext == null) outlinetext = text;
            GUIContent content1 = new GUIContent(outlinetext);
            GUIStyle style = GUI.skin.label;
            style.alignment = TextAnchor.MiddleCenter;
            Vector2 size = style.CalcSize(content);
            Vector3 pos = Camera.main.WorldToScreenPoint(worldpos);
            pos.y = Screen.height - pos.y;
            if (pos.z >= 0)
            {
                GUI.color = Color.black;
                GUI.Label(new Rect((pos.x - size.x / 2) + 1, pos.y + 1, size.x, size.y), content1);
                GUI.Label(new Rect((pos.x - size.x / 2) - 1, pos.y - 1, size.x, size.y), content1);
                GUI.Label(new Rect((pos.x - size.x / 2) + 1, pos.y - 1, size.x, size.y), content1);
                GUI.Label(new Rect((pos.x - size.x / 2) - 1, pos.y + 1, size.x, size.y), content1);
                GUI.color = textcolor;
                GUI.Label(new Rect(pos.x - size.x / 2, pos.y, size.x, size.y), content);
                GUI.color = Color.black;
            }
        }

        public static Vector3 WorldToScreen(Vector3 worldpos)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(worldpos);
            pos.y = Screen.height - pos.y;
            return new Vector3(pos.x, pos.y);
        }

        public static void DrawOutlineLabel(Vector2 rect, Color textcolor, Color outlinecolor, string text, string outlinetext = null)
        {
            GUIContent content = new GUIContent(text);
            if (outlinetext == null) outlinetext = text;
            GUIContent content1 = new GUIContent(outlinetext);
            GUIStyle style = GUI.skin.label;
            Vector2 size = style.CalcSize(content);
            GUI.color = Color.black;
            GUI.Label(new Rect((rect.x) + 1, rect.y + 1, size.x, size.y), content1);
            GUI.Label(new Rect((rect.x) - 1, rect.y - 1, size.x, size.y), content1);
            GUI.Label(new Rect((rect.x) + 1, rect.y - 1, size.x, size.y), content1);
            GUI.Label(new Rect((rect.x) - 1, rect.y + 1, size.x, size.y), content1);
            GUI.color = textcolor;
            GUI.Label(new Rect(rect.x, rect.y, size.x, size.y), content);
            GUI.color = Color.black;
        }

        public static void Draw3DBox(Bounds b, Color color)
        {
            Vector3[] pts = new Vector3[8];
            pts[0] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[1] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[2] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[3] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x + b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));
            pts[4] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z + b.extents.z));
            pts[5] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y + b.extents.y, b.center.z - b.extents.z));
            pts[6] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z + b.extents.z));
            pts[7] = Camera.main.WorldToScreenPoint(new Vector3(b.center.x - b.extents.x, b.center.y - b.extents.y, b.center.z - b.extents.z));

            for (int i = 0; i < pts.Length; i++) pts[i].y = Screen.height - pts[i].y;

            GL.PushMatrix();
            GL.Begin(1);
            DrawMaterial.SetPass(0);
            GL.End();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Begin(1);
            DrawMaterial.SetPass(0);
            GL.Color(color);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[2].x, pts[2].y, 0f);
            GL.Vertex3(pts[0].x, pts[0].y, 0f);
            GL.Vertex3(pts[3].x, pts[3].y, 0f);
            GL.Vertex3(pts[1].x, pts[1].y, 0f);
            GL.Vertex3(pts[7].x, pts[7].y, 0f);
            GL.Vertex3(pts[5].x, pts[5].y, 0f);
            GL.Vertex3(pts[6].x, pts[6].y, 0f);
            GL.Vertex3(pts[4].x, pts[4].y, 0f);

            GL.End();
            GL.PopMatrix();

        }


        // thanks
        public static void DrawColor(Rect position, Color color)
        {
            var backgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = color;
            GUI.Box(position, GUIContent.none, textureStyle);
            GUI.backgroundColor = backgroundColor;
        }

        public static void DrawColorLayout(Color color, GUILayoutOption[] options = null)
        {
            GUI.skin = AssetUtilities.Skin;
            var backgroundColor = GUI.backgroundColor;
            GUI.backgroundColor = color;
            GUILayout.Button(" ", textureStyle, options);
            GUI.backgroundColor = backgroundColor;
        }

        public static void ApplyShader(Shader shader, GameObject pgo, Color32 VisibleColor, Color32 OccludedColor)
        {
            if (shader == null) return;

            Renderer[] rds = pgo.GetComponentsInChildren<Renderer>();

            for (int j = 0; j < rds.Length; j++)
            {
                Material[] materials = rds[j].materials;

                for (int k = 0; k < materials.Length; k++)
                {
                    materials[k].shader = shader;
                    materials[k].SetColor("_ColorVisible", VisibleColor);
                    materials[k].SetColor("_ColorBehind", OccludedColor);
                }
            }
        }

        public static void Log(object s) =>
           Load.Log.LogWarning(s.ToString());

        public static void RemoveShaders(GameObject pgo)
        {
            if (Shader.Find("Standard") == null) return;

            Renderer[] rds = pgo.GetComponentsInChildren<Renderer>();

            for (int j = 0; j < rds.Length; j++)
            {
                if (!(rds[j].material.shader != Shader.Find("Standard"))) continue;

                Material[] materials = rds[j].materials;

                for (int k = 0; k < materials.Length; k++)
                {
                    materials[k].shader = Shader.Find("Standard");
                }
            }
        }


        // binjector moment
        public static void OverrideMethod(Type defaultClass, Type overrideClass, string method, BindingFlags bindingflag1, BindingFlags bindingflag2, BindingFlags overrideflag1, BindingFlags overrideflag2)
        {
            string overriddenmethod = "OV_" + method;

            var MethodToOverride = defaultClass.GetMember(method, MemberTypes.Method, bindingflag1 | bindingflag2).Cast<MethodInfo>();

            OverrideHelper.RedirectCalls(MethodToOverride.ToArray()[0], overrideClass.GetMethod(overriddenmethod, overrideflag1 | overrideflag2));
        }
        public static void resetMenu()
        {

        }
        // big maths
        public static void DrawCircle(Color Col, Vector2 Center, float Radius)
        {
            GL.PushMatrix();
            DrawMaterial.SetPass(0);
            GL.Begin(1);
            GL.Color(Col);
            for (float num = 0f; num < 6.28318548f; num += 0.05f)
            {
                GL.Vertex(new Vector3(Mathf.Cos(num) * Radius + Center.x, Mathf.Sin(num) * Radius + Center.y));
                GL.Vertex(new Vector3(Mathf.Cos(num + 0.05f) * Radius + Center.x, Mathf.Sin(num + 0.05f) * Radius + Center.y));
            }
            GL.End();
            GL.PopMatrix();
        }

        public static Random Random = new Random();
    }
}
