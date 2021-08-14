using System;

namespace Plugins.HabObject.Customizable.Attributs
{
    public class TRangeFloat : TField
    {
        public string Name { get; }
        public float Min { get; }
        public float Max { get; }
        public float[] Step { get; }

        public TRangeFloat(string name, float min, float max, float[] step)
        {
            if(min>max)
                throw new Exception("wrong value");
            Name = name;
            Min = min;
            Max = max;
            Step = step;
        }
    }
}