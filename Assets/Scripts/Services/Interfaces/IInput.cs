using Infrastructure.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Services.Interfaces
{
    public interface IInput
    {
        Vector2 AxisMove { get; }
        float DeltaScroll { get; }
        event UnityAction<bool> ChangeMove;
        event UnityAction InventoryButton;
        event UnityAction MenuButtonClick;
        event UnityAction Intractable;
        event UnityAction MainAttackClick;
        event UnityAction MainAttackHold;
        event UnityAction MainAttackUnclick;
        void Update();
        void InitData(DataControl dataControl);
    }
}