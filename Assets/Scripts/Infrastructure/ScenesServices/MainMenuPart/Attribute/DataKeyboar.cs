using System;
using UnityEngine;

namespace Infrastructure.ScenesServices.MainMenuPart.Attribute
{
    public class DataKeyboar : DataAttribute
    {
        public Type TypeInput { get; private set; }
        
        public DataKeyboar(string nameProperty, Type typeInput) : base(nameProperty)
        {
            if (typeInput != typeof(KeyCode))
                throw new Exception("Wrong type for TypeInput in DataKeyboard");
            TypeInput = typeInput;
        }
    }
}