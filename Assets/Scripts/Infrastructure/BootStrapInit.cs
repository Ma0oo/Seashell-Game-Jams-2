using Infrastructure.Configs;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using Plugins.HubObject.GlobalSystem;
using UnityEngine;

namespace Infrastructure
{
    public class BootStrapInit : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SceneSetConfig _sceneConfig;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);

            GameStateMachine gameStateMachine = new GameStateMachine(this);
            
            RegisterConfings();
            ServicesLocator.MainContainer.RegisterSingle<ICoroutineRunner>(this);
            ServicesLocator.MainContainer.RegisterSingle(gameStateMachine);
            
            gameStateMachine.Enter<InitGame>();
        }

        private void RegisterConfings()
        {
            ServicesLocator.MainContainer.RegisterSingle(_sceneConfig);
        }
    }
}