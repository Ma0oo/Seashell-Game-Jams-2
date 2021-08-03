using Factorys;
using HabObjects;
using Infrastructure.Configs;
using Infrastructure.GameStateMachines.Interfaces;
using Infrastructure.ScenesServices.Lobby;
using Infrastructure.ScenesServices.Lobby.Signals;
using Mechanics;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using UnityEngine;

namespace Infrastructure.GameStateMachines.States
{
    public class Lobby : IEnterState
    {
        public const string ChanelLobbyId = "ChanelLobby";
        
        [DI] private SceneLoader _sceneLoader;
        [DI] private SceneSetConfig _sceneSet;
        [DI] private Curtain _curtain;
        
        private DiServices _mainDiServices = DiServices.MainContainer;
        private EventChanel _chanelLobby = new EventChanel();

        public void Enter()
        {
            RegisterSignal();
            RegisterDI(new DataPlayerProvider());
            _sceneLoader.Load(_sceneSet.Lobby, _curtain.Unfade);   
        }

        public void Exit()
        {
            _chanelLobby.Fire(new LobbyStartExit());
            UnregisterDI();
            UnregisterSignal();
        }

        private void UnregisterDI()
        {
            _mainDiServices.RemoveSingel<DataPlayerProvider>();
            _mainDiServices.RemoveSingel<EventChanel>(ChanelLobbyId);
            _mainDiServices.RemoveSingel<Actor>(DIConstID.PlayerId);
            _mainDiServices.RemoveSingel<Canvas>(HudFactory.IdPlayerCanvas);
        }

        private void UnregisterSignal()
        {
            _chanelLobby.UnregisterSignal<PlayerSpawned>();
            _chanelLobby.UnregisterSignal<LobbyStartExit>();
            _chanelLobby.UnregisterSignal<LobbyFinishEnter>();
        }

        private void RegisterDI(DataPlayerProvider dataPlayerProvider)
        {
            _mainDiServices.RegisterSingle(dataPlayerProvider);
            _mainDiServices.RegisterSingle(_chanelLobby, ChanelLobbyId);
            
            _mainDiServices.InjectSingle(dataPlayerProvider);
        }
        
        private void RegisterSignal()
        {
            _chanelLobby.RegisterSignal<PlayerSpawned>();   
            _chanelLobby.RegisterSignal<LobbyFinishEnter>();
            _chanelLobby.RegisterSignal<LobbyStartExit>();
        }
    }
}