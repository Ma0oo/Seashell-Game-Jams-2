using System.Collections.Generic;
using Infrastructure.GameStateMachines.Interfaces;
using Infrastructure.Services;
using Plugins.HubObject.GlobalSystem;
using Object = System.Object;

namespace Infrastructure.GameStateMachines.States
{
    public class InitGame : IEnterState
    {
        private ServicesLocator _servicesLocator = ServicesLocator.MainContainer;

        [DI] private GameStateMachine _gameStateMachine;

        public void Enter()
        {
            List<Object> _objectToInjectDI = new List<Object>();
            
            _objectToInjectDI.Add(CreateDataProvider());
            _objectToInjectDI.Add(RegisterProfileDataProvider());
            _objectToInjectDI.Add(RegisterSceneLoader());
            
            Inject(_objectToInjectDI);
            
            _gameStateMachine.Enter<MainMenu>();
        }

        public void Exit()
        {
            
        }

        private void Inject(List<object> _objectToInjectDI)
        {
            foreach (var @object in _objectToInjectDI) _servicesLocator.InjectSingle(@object);
        }

        private SceneLoader RegisterSceneLoader()
        {
            var loader = new SceneLoader();
            _servicesLocator.RegisterSingle(loader);
            return loader;
        }

        private ProfileProvider RegisterProfileDataProvider()
        {
            ProfileProvider profileProvider = new ProfileProvider();
            _servicesLocator.RegisterSingle(profileProvider);
            return profileProvider;
        }

        private DataProvider CreateDataProvider()
        {
            DataProvider dataProvider = new DataProvider();
            _servicesLocator.RegisterSingle(dataProvider);
            return dataProvider;
        }
    }
}