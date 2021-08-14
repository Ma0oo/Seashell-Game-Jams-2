using System;

namespace Plugins.HabObject.Customizable.Attributs
{
    public class TObject : TField
    {
        public string Name { get; }

        public TObject(string name) => Name = name;
    }
}