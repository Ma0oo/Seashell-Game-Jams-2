using Infrastructure.GameStateMachines;
using Mechanics;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Infrastructure.ScenesServices.MainMenuPart
{
    public class ChangerStateMainMenu
    {
        [DI] private GameStateMachine _gameStateMachine;
        [DI] private Curtain _curtain;
        
        public void Start() => _curtain.Fade(()=>_gameStateMachine.Enter<GameStateMachines.States.Lobby>());

        public void Exit() => _curtain.Fade(() => Application.Quit());
    }
}