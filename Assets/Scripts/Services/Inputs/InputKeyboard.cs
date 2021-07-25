using Services.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Services.Inputs
{
    public class InputKeyboard : IInput
    {
        private const string VerticalAxis = "Vertical";
        private const string HorizontalAxis = "Horizontal";
        
        public Vector2 AxisMove => new Vector2(Input.GetAxis(HorizontalAxis), Input.GetAxis(VerticalAxis));
        public event UnityAction<bool> ChangeMove;
        public event UnityAction Intractable;

        private bool isMove = false;
        
        public void Update()
        {
            float magnitude = AxisMove.magnitude;
            if (magnitude > 0.1f && !isMove) ChangeMove?.Invoke(isMove = true);
            else if(magnitude < 0.1f && isMove) ChangeMove?.Invoke(isMove = false);
            
            if(Input.GetKeyDown(KeyCode.E)) Intractable?.Invoke();
        }
    }
}