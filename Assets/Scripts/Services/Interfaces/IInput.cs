using UnityEngine;
using UnityEngine.Events;

namespace Services.Interfaces
{
    public interface IInput
    {
        Vector2 AxisMove { get; }
        event UnityAction<bool> ChangeMove;
        event UnityAction Intractable;
        void Update();
    }
}