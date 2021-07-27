using System;

namespace Infrastructure.Scenes.MainMenuPart.Attribute
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public abstract class DataAttribute : System.Attribute
    {
        public readonly string NameProperty;

        public DataAttribute(string nameProperty)
        {
            NameProperty = nameProperty;
        }
    }
}