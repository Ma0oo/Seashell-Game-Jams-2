using Infrastructure.ScenesServices.MainMenuPart.Attribute;
using UnityEngine;

namespace Infrastructure.Data
{
    public class DataControl : IData
    {
        public string Name => "ControlData";
        
        [DataKeyboar("Up", typeof(KeyCode))] 
        public KeyCode keyMoveUp = KeyCode.W;
        
        [DataKeyboar("Down", typeof(KeyCode))] 
        public KeyCode keyMoveDown = KeyCode.S;
        
        [DataKeyboar("Right", typeof(KeyCode))] 
        public KeyCode keyMoveRight = KeyCode.D;
        
        [DataKeyboar("Left", typeof(KeyCode))] 
        public KeyCode keyMoveLeft = KeyCode.A;
        
        [DataKeyboar("Interact", typeof(KeyCode))]
        public KeyCode keyInteract = KeyCode.E;

        [DataKeyboar("Inventory Button", typeof(KeyCode))]
        public KeyCode Inventory = KeyCode.I;

        [DataKeyboar("Attack", typeof(KeyCode))]
        public KeyCode Attack = KeyCode.Mouse0;

        [DataKeyboar("Pause button", typeof(KeyCode))]
        public KeyCode Pause = KeyCode.Escape;
    }
}