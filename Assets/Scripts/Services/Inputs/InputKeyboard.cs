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
        public event UnityAction InventoryButton;
        public event UnityAction MenuButtonClick;
        public event UnityAction Intractable;

        private bool isMove = false;
        
        public void Update()
        {
            ChangeMoveEvent();
            IntractableEvent();
            InventoryEvent();
            MenuEvent();
        }

        private void MenuEvent()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                MenuButtonClick?.Invoke();
        }

        private void ChangeMoveEvent()
        {
            float magnitude = AxisMove.magnitude;
            if (magnitude > 0.1f && !isMove) ChangeMove?.Invoke(isMove = true);
            else if (magnitude < 0.1f && isMove) ChangeMove?.Invoke(isMove = false);
        }

        private void IntractableEvent()
        {
            if (Input.GetKeyDown(KeyCode.E)) Intractable?.Invoke();
        }

        private void InventoryEvent()
        {
            if (Input.GetKeyDown(KeyCode.I)) InventoryButton?.Invoke();
        }
    }
}