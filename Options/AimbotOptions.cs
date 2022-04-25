using Aerolt.Classes;
using System.Collections.Generic;
using UnityEngine;

namespace Aerolt.Options
{
    public class AimbotOptions
    {
        public bool SilentAim = false;
        public bool SilentAimInfo = true;
        public int AimpointMultiplier = 1;
        public int HitboxSize = 15;
        public KeyCode AimlockKey = KeyCode.F;
        public bool OnlyVisible = false;
        public bool Aimlock = false;
        public bool Mouse1Aimbot = false;
        public bool AimlockLimitFOV = false;
        public int AimlockFOV = 200;
        public bool AimlockDrawFOV = true;
        public bool SilentAimLimitFOV = false;
        public int SilentAimFOV = 200;
        public bool SilentAimDrawFOV = true;
        public bool ExpandHitboxes = true;
        public int HitChance = 100;
    }
}
