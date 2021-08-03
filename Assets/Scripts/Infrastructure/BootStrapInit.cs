using Infrastructure.Configs;
using Infrastructure.Data;
using Infrastructure.GameStateMachines;
using Infrastructure.GameStateMachines.States;
using Mechanics;
using Plugins.HabObject.DIContainer;
using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure
{
    public class BootStrapInit : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private SceneSetConfig _sceneConfig;
        [SerializeField] private ConfigMixerGroup _configMixerGroup;
        [SerializeField] private ConfigPrefabs _prefabs;
        [SerializeField] private AudioMixer _mixerAudio;
        [SerializeField] private Curtain _curtain;
        
        private void Awake()
        {
            DontDestroyOnLoad(this);

            GameStateMachine gameStateMachine = new GameStateMachine(this);
            
            RegisterConfings();
            DiServices.MainContainer.RegisterSingle<ICoroutineRunner>(this);
            DiServices.MainContainer.RegisterSingle(gameStateMachine);
            DiServices.MainContainer.RegisterSingle(_mixerAudio);
            DiServices.MainContainer.RegisterSingle(_curtain);

            gameStateMachine.Enter<InitGame>();
        }

        private void RegisterConfings()
        {
            DiServices.MainContainer.RegisterSingle(_sceneConfig);
            DiServices.MainContainer.RegisterSingle(_prefabs);
            DiServices.MainContainer.RegisterSingle(_configMixerGroup);
        }
    }
}