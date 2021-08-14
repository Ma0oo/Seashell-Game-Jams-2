using System;

namespace Plugins.HabObject.Customizable.Attributs
{
    public class TFloat : TField
    {
        public string Name { get; }
        public float[] Step { get; }

        public TFloat(string name, float[] step)
        {
            Name = name;
            Step = step;
        }
    }
}