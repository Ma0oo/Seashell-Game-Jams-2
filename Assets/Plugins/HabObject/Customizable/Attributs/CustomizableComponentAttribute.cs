using System;

namespace Plugins.HabObject.Customizable.Attributs
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomizableComponentAttribute : Attribute
    {
        public string NameComponent { get; }
        public int OrderSort { get; }

        public CustomizableComponentAttribute(string name, int orderSort)
        {
            NameComponent = name;
            OrderSort = orderSort;
        }
    }
}