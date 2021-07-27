using System;
using Infrastructure.GameStateMachines.States;
using Infrastructure.Scenes.MainMenuPart.Signals;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Scenes.MainMenuPart.Mono
{
    public class ButtonsMainMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _exitButton;
        
        [DI(MainMenu.EventChanelId)] private EventChanel _event;
        [DI] private ChangerStateMainMenu _changerStateMain;

        private void Awake()
        {
            _event.AddListen<WindowAction>(OnWindowOpen);
            _startButton.onClick.AddListener(()=>_changerStateMain.Start());
            _exitButton.onClick.AddListener(()=>_changerStateMain.Exit());
        }

        private void OnWindowOpen(WindowAction obj)
        {
            if (obj.MakeType == WindowAction.Action.Open)
                _canvasGroup.interactable = false;
            else
                _canvasGroup.interactable = true;
        }

        private void OnDestroy() => _event.RemoveListen<WindowAction>(OnWindowOpen);
    }
}