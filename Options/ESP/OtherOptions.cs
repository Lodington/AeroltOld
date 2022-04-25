using System.Collections.Generic;
using UnityEngine;

namespace Aerolt.Options.ESP
{
    public class OtherOptions
    {
        // Custom Options
        #region Bed
        public bool Claimed = true;
        public bool OnlyUnclaimed = false;
        #endregion
        #region Items
        public bool ListClumpedItems = false;
        public float DistanceThreshold = 3;
        public int CountThreshold = 5;
        #endregion
        #region Player
        public bool Weapon = true;
        public bool ViewHitboxes = true;
        #endregion
        #region Vehicle
        public bool VehicleLocked = true;
        public bool OnlyUnlocked = false;
        #endregion
        #region Storage
        public bool ShowLocked = true;
        #endregion
        #region Other
        public Dictionary<string, Color32> GlobalColors = new Dictionary<string, Color32>();
        #endregion
    }
}
