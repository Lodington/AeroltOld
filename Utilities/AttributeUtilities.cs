using Aerolt.Attributes;
using System;
using System.Reflection;

namespace Aerolt.Utilities
{
    public static class AttributeUtilities
    {
        public static void LinkAttributes()
        {
            foreach (Type T in Assembly.GetExecutingAssembly().GetTypes())
            {
                // Collect and add components marked with the attribute
                if (T.IsDefined(typeof(Comp), false))
                    Load.CO.AddComponent(T);
            }
        }
    }
}