using Infrastructure.Configs;
using Infrastructure.GameStateMachines.Interfaces;
using Plugins.HabObject.DIContainer;

namespace Infrastructure.GameStateMachines.States
{
    public class RandomDungeon : IPayLoadedState<DataDungeon>
    {
        private DataDungeon _dataLevel;

        [DI] private SceneSetConfig _scenes;
        [DI] private SceneLoader _sceneLoader;
        
        public void Enter(DataDungeon payLoaded)
        {
            _dataLevel = payLoaded;
            DiServices.MainContainer.RegisterSingle(_dataLevel);
            _sceneLoader.Load(_scenes.Game);
        }

        public void Exit()
        {
            DiServices.MainContainer.RemoveSingel<DataDungeon>();
        }
    }
}