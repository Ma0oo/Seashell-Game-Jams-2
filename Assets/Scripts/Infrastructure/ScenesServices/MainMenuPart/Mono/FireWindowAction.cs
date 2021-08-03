using Infrastructure.GameStateMachines.States;
using Infrastructure.ScenesServices.MainMenuPart.Signals;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.ScenesServices.MainMenuPart.Mono
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