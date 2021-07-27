using System;
using Infrastructure.GameStateMachines.States;
using Infrastructure.Scenes.MainMenuPart.Signals;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Infrastructure.Scenes.MainMenuPart.Mono
{
    public class ListenWindowAction : MonoBehaviour
    {
        [SerializeField] private WindowAction.Action _targetAction;
        [SerializeField] private WindowAction.WindowType _targetType;
        [SerializeField] private UnityEvent _action;
        
        [DI(MainMenu.EventChanelId)] private EventChanel _chanel;

        private void Awake() => _chanel.AddListen<WindowAction>(OnWindowAction);

        private void OnWindowAction(WindowAction action)
        {
            if(action.MakeType == _targetAction && action.Type == _targetType)
                _action.Invoke();
        }
    }
}