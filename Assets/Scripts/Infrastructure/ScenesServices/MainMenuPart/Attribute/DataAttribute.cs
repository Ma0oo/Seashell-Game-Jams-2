using System;

namespace Infrastructure.ScenesServices.MainMenuPart.Attribute
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