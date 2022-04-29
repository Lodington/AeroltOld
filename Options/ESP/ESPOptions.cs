using Aerolt.Classes;

namespace Aerolt.Options.ESP
{
    // options for each esp object
    public class ESPOptions
    {
        public bool Enabled = false;
        public bool dataCollected = false;

        public bool showTeleporter = true;
        public bool showBarrels = true;
        public bool showPressurePlates = true;
        public bool showScrappers = true;
        public bool showChest = true;
        public bool showMultiShops = true;
        public bool showEnemies = true;

        public ShaderType ChamType = ShaderType.Material;
    }
}
