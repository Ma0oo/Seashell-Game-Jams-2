using System;
using System.Runtime.InteropServices;
using Infrastructure.GameStateMachines.States;
using Infrastructure.Scenes.MainMenuPart.Signals;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Scenes.MainMenuPart.Mono
{
    public class FireWindowAction : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowAction.WindowType _typeWindow;
        [SerializeField] private WindowAction.Action _action;

        [DI(MainMenu.EventChanelId)] private EventChanel _eventMainMenu;
        
        private void Awake() => _button.onClick.AddListener(()=>_eventMainMenu.Fire(new WindowAction(_action, _typeWindow)));
    }
}