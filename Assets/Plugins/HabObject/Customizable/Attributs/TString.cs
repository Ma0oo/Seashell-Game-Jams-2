using System;

namespace Plugins.HabObject.Customizable.Attributs
{
    public class TString : TField
    {
        public string Name { get; }
        public int Space { get; }

        public TString(string name, int space)
        {
            if(space<1)
                throw new Exception("Wrong space");
            Name = name;
            Space = space;
        }
    }
}