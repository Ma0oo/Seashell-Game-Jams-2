﻿using Plugins.HabObject.GlobalSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Services.Interfaces
{
    public interface IInput
    {
        Vector2 AxisMove { get; }
        event UnityAction<bool> ChangeMove;
        event UnityAction InventoryButton;
        event UnityAction MenuButtonClick;
        event UnityAction Intractable;
        void Update();
    }
}