using System;

namespace Plugins.HabObject.Customizable.Attributs
{
    public class TInt : TField
    {
        public string Name { get; }
        public int[] Step { get; }

        public TInt(string name, int[] step)
        {
            Name = name;
            Step = step;
        }
    }
}