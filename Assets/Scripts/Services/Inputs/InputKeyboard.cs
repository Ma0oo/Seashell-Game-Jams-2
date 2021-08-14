using Infrastructure.Data;
using Services.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Services.Inputs
{
    public class InputKeyboard : IInput
    {
        private const string VerticalAxis = "Vertical";
        private const string HorizontalAxis = "Horizontal";
        
        public Vector2 AxisMove => 
            new Vector2(
                GetValueByKey(_dataControl.keyMoveLeft, _dataControl.keyMoveRight),
                GetValueByKey(_dataControl.keyMoveDown, _dataControl.keyMoveUp));

        public float DeltaScroll => Input.mouseScrollDelta.y;
        public event UnityAction<bool> ChangeMove;
        public event UnityAction InventoryButton;
        public event UnityAction MenuButtonClick;
        public event UnityAction Intractable;
        public event UnityAction MainAttackClick;
        public event UnityAction MainAttackHold;
        public event UnityAction MainAttackUnclick;

        private bool isMove = false;
        private DataControl _dataControl ;

        public InputKeyboard()
        {
            
        }
        
        public void Update()
        {
            if(_dataControl == null)
                return;

            ChangeMoveEvent();
            IntractableEvent();
            InventoryEvent();
            MenuEvent();
            MainAttackClickEvents();
        }

        private float GetValueByKey(KeyCode negativeKey, KeyCode positiveKey)
        {
            if (Input.GetKey(negativeKey) && Input.GetKey(positiveKey)) 
                return 0;
            else if (Input.GetKey(negativeKey))
                return -1;
            else if (Input.GetKey(positiveKey))
                return 1;
            else
                return 0;
        }

        public void InitData(DataControl dataControl)
        {
            if (dataControl == null)
                throw null;
            
            _dataControl = dataControl;
        }

        private void MainAttackClickEvents()
        {
            if(Input.GetKeyDown(_dataControl.Attack))
                MainAttackClick?.Invoke();
            if(Input.GetKey(_dataControl.Attack))
                MainAttackHold?.Invoke();
            if(Input.GetKeyUp(_dataControl.Attack))
                MainAttackUnclick?.Invoke();
        }

        private void MenuEvent()
        {
            if (Input.GetKeyDown(_dataControl.Pause))
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
            if (Input.GetKeyDown(_dataControl.keyInteract)) Intractable?.Invoke();
        }

        private void InventoryEvent()
        {
            if (Input.GetKeyDown(_dataControl.Inventory)) InventoryButton?.Invoke();
        }
    }
}