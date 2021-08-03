using System;
using HabObjects;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.Interface;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using Infrastructure.ScenesServices.Lobby;
using Mechanics;
using Plugins.HabObject.DIContainer;
using UnityEngine;
using UnityEngine.UI;

namespace Huds
{
    public class MenuEsc : Hud
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _menuButton;
        
        [DI] private Curtain _curtain;
        [DI] private DataPlayerProvider _dataPlayerProvider;
        [DI(DIConstID.PlayerId)] private Actor _player;
        [DI] private GameStateMachine _gameStateMachine;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OnExitClick);
            _menuButton.onClick.AddListener(OnMenuClick);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OnExitClick);
            _menuButton.onClick.RemoveListener(OnMenuClick);
        }

        private void OnExitClick()
        {
            _dataPlayerProvider.SavePlayer(_player);
            _curtain.Fade(() => Application.Quit());
        }

        private void OnMenuClick()
        {
            _dataPlayerProvider.SavePlayer(_player);
            _curtain.Fade(()=>_gameStateMachine.Enter<MainMenu>());
        }
    }
}