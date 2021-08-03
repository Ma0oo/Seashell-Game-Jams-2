using Infrastructure.Configs;
using Infrastructure.GameStateMachines.Interfaces;
using Infrastructure.ScenesServices.MainMenuPart;
using Infrastructure.ScenesServices.MainMenuPart.Signals;
using Mechanics;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using UnityEngine;

namespace Infrastructure.GameStateMachines.States
{
    public class MainMenu : IEnterState
    {
        public const string EventChanelId = "ChanelOfMainMenu";
        
        [DI] private SceneLoader _sceneLoader;
        [DI] private SceneSetConfig _scenes;
        [DI] private Curtain _curtain;

        private DiServices _mianDiServices = DiServices.MainContainer;
        private EventChanel _chanelMainMenu = new EventChanel();

        public void Enter()
        {
            RegisterSignal();
            RegisterDI(new TransiterDataForUnity(),  new ChangerStateMainMenu());
            _sceneLoader.Load(_scenes.MainMenu, () => _curtain.Unfade());
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
            _mianDiServices.RemoveSingel<TransiterDataForUnity>();
            _mianDiServices.RemoveSingel<ChangerStateMainMenu>();
            _mianDiServices.RemoveSingel<EventChanel>(EventChanelId);
        }

        private void RegisterSignal()
        {
            _chanelMainMenu.RegisterSignal<ProfileUpdate>();
            _chanelMainMenu.RegisterSignal<WindowAction>();
        }

        private void RegisterDI(TransiterDataForUnity transitDataForUnity, ChangerStateMainMenu changerStateMainMenu)
        {
            _mianDiServices.RegisterSingle(_chanelMainMenu, EventChanelId);
            _mianDiServices.RegisterSingle(transitDataForUnity);
            _mianDiServices.RegisterSingle(changerStateMainMenu);
            
            _mianDiServices.InjectSingle(transitDataForUnity);            
            _mianDiServices.InjectSingle(changerStateMainMenu);
        }
    }
}