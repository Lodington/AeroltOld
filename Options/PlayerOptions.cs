namespace Aerolt.Options
{
    public class PlayerOptions
    {
       

        public bool godModeEnabled = false;
        public bool skillToggle = false;
        public bool noClipEnabled = false;
        public bool alwaysSprint = false;
        public bool aimbotEnabled = false;

        public bool doMassiveDamage = false;
        public bool MaxCrit = false;

        public float lvlDamage;
        public float lvlCrit;

        public int amountOfItemsToGive = 5;

        public float CharactersBaseDamage;
        public bool doDamageModifier = false;
        public float damageModifier = 1;
        public bool doAttackSpeed = false;
        public float attackSpeed = 1;

        public uint moneyToGive = 100;
        public uint xpToGive = 100;
        public uint _LunarCoinsSelf = 100;
        public uint _LunarCoinsAll = 100;
    }
}
