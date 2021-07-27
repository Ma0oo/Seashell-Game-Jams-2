using Infrastructure.Configs;
using Infrastructure.GameStateMachines.Interfaces;
using Plugins.HubObject.GlobalSystem;

namespace Infrastructure.GameStateMachines.States
{
    public class Lobby : IEnterState
    {
        [DI] private SceneLoader _sceneLoader;
        [DI] private SceneSetConfig _sceneSet;

        public void Enter()
        {
            _sceneLoader.Load(_sceneSet.Lobby);   
        }

        public void Exit()
        {
            
        }
    }
}