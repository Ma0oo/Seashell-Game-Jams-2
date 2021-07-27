using Infrastructure.Configs;
using Infrastructure.GameStateMachines.Interfaces;
using Infrastructure.Scenes.MainMenuPart.Mono;
using Infrastructure.Scenes.MainMenuPart.Signals;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;

namespace Infrastructure.GameStateMachines.States
{
    public class MainMenu : IEnterState
    {
        public const string EventChanelId = "ChanelOfMainMenu";
        
        [DI] private SceneLoader _sceneLoader;
        [DI] private SceneSetConfig _scenes;

        private ServicesLocator _mianServices = ServicesLocator.MainContainer;
        private EventChanel _chanelMainMenu = new EventChanel();

        public void Enter()
        {
            RegisterSignal();
            RegisterDI(new TransiterDataForUnity(),  new ChangerStateMainMenu());
            _sceneLoader.Load(_scenes.MainMenu, () => { });
        }

        public void Exit()
        {
            UnregisterSignal();
            UnRegisterDi();
        }

        private void UnregisterSignal()
        {
            _chanelMainMenu.UnregisterSignal<ProfileUpdate>();
            _chanelMainMenu.UnregisterSignal<WindowAction>();
        }

        private void UnRegisterDi()
        {
            _mianServices.RemoveSingel<TransiterDataForUnity>();
            _mianServices.RemoveSingel<ChangerStateMainMenu>();
        }

        private void RegisterSignal()
        {
            _chanelMainMenu.RegisterSignal<ProfileUpdate>();
            _chanelMainMenu.RegisterSignal<WindowAction>();
        }

        private void RegisterDI(TransiterDataForUnity transitDataForUnity, ChangerStateMainMenu changerStateMainMenu)
        {
            _mianServices.RegisterSingle(_chanelMainMenu, EventChanelId);
            _mianServices.RegisterSingle(transitDataForUnity);
            _mianServices.RegisterSingle(changerStateMainMenu);
            
            _mianServices.InjectSingle(transitDataForUnity);            
            _mianServices.InjectSingle(changerStateMainMenu);
        }
    }
}