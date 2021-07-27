using Plugins.HubObject.GlobalSystem;
using UnityEngine;

namespace Infrastructure.GameStateMachines.States
{
    public class ChangerStateMainMenu
    {
        [DI] private GameStateMachine _gameStateMachine;
        
        public void Start() => _gameStateMachine.Enter<Lobby>();

        public void Exit() => Application.Quit();
    }
}