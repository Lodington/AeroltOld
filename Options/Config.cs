using Aerolt.Options.ESP;
using System.Collections.Generic;

namespace Aerolt.Options
{
    public class Config
    {
        public OtherOptions GlobalOptions = new OtherOptions();

        public ItemOptions itemoptions = new ItemOptions();
        public ESPOptions espoptions = new ESPOptions();

        public AimbotOptions AimbotOptions = new AimbotOptions();
        public PlayerOptions PlayerOptions = new PlayerOptions();
        public MiscOptions MiscOptions = new MiscOptions();

    }
}
