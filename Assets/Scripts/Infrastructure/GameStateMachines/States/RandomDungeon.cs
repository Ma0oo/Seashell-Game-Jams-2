using System.Collections.Generic;
using Factorys;
using HabObjects;
using Infrastructure.Configs;
using Infrastructure.GameStateMachines.Interfaces;
using Infrastructure.ScenesServices.Lobby;
using Infrastructure.ScenesServices.Lobby.Signals;
using Infrastructure.Services;
using Plugins.HabObject.DIContainer;
using Plugins.HabObject.GlobalSystem;
using UnityEngine;
using Object = System.Object;

namespace Infrastructure.GameStateMachines.States
{
    public class RandomDungeon : IPayLoadedState<DataDungeon>
    {
        public const string IdChanelLevel = "ChanelDungeon";
        
        [DI] private SceneSetConfig _scenes;
        [DI] private SceneLoader _sceneLoader;

        private DataDungeon _dataLevel;
        private EventChanel _chanelLevel = new EventChanel();
        
        public void Enter(DataDungeon payLoaded)
        {
            _dataLevel = payLoaded;
            
            _chanelLevel.RegisterSignal<PlayerSpawned>();
            
            RegisterDI();

            _sceneLoader.Load(_scenes.Game);
        }

        public void Exit()
        {
            DiServices.MainContainer.RemoveSingel<DataDungeon>();
            DiServices.MainContainer.RemoveSingel<DataPlayerProvider>();
            DiServices.MainContainer.RemoveSingel<EventChanel>(IdChanelLevel);
            DiServices.MainContainer.RemoveSingel<Actor>(DIConstID.PlayerId);
            DiServices.MainContainer.RemoveSingel<Canvas>(HudFactory.IdPlayerCanvas);
            
            _chanelLevel.UnregisterSignal<PlayerSpawned>();
        }

        private void RegisterDI()
        {
            List<Object> objectToInject = new List<object>(); 
            
            objectToInject.Add(CreateDI<DataPlayerProvider>());
            DiServices.MainContainer.RegisterSingle(_dataLevel);
            DiServices.MainContainer.RegisterSingle(_chanelLevel, IdChanelLevel);

            foreach (var o in objectToInject) DiServices.MainContainer.InjectSingle(o);
        }

        private T CreateDI<T>() where T : class, new()
        {
            T result = new T();
            DiServices.MainContainer.RegisterSingle(result);
            return result;
        }
    }
}