using System;

namespace Plugins.HabObject.Customizable.Attributs
{
    public class TRangeInt : TField
    {
        public string Name { get; }
        public int Min { get; }
        public int Max { get; }
        public int[] Step { get; }

        public TRangeInt(string name, int min, int max, int[] step)
        {
            if(min>max)
                throw new Exception("Wrong value");
            Name = name;
            Min = min;
            Max = max;
            Step = step;
        }
    }
}