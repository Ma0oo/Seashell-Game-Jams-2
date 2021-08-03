using System.Collections;
using System.Collections.Generic;
using Infrastructure.GameStateMachines.Interfaces;
using Infrastructure.ScenesServices.Lobby;
using Infrastructure.Services;
using Mechanics;
using Plugins.HabObject.DIContainer;
using Services.Inputs;
using Services.Interfaces;
using Services.Sound;
using Object = System.Object;

namespace Infrastructure.GameStateMachines.States
{
    public class InitGame : IEnterState
    {
        private DiServices _diServices = DiServices.MainContainer;

        [DI] private GameStateMachine _gameStateMachine;
        [DI] private ICoroutineRunner _coroutineRuuner;
        [DI] private Curtain _curtain;

        public void Enter()
        {
            CreateSignal();
            CreateDi();
            _curtain.Fade(()=>_gameStateMachine.Enter<MainMenu>());
        }

        public void Exit()
        {
            
        }

        private void CreateSignal()
        {
            
        }


        private void CreateDi()
        {
            List<Object> _objectToInjectDI = new List<Object>();

            _objectToInjectDI.Add(CreateDI<DataProvider>());
            _objectToInjectDI.Add(CreateDI<ProfileProvider>());
            _objectToInjectDI.Add(CreateDI<SceneLoader>());
            _objectToInjectDI.Add(CreateDI<GameTime>());
            _objectToInjectDI.Add(CreateDI<SoundSystem>());
            _objectToInjectDI.Add(CreateDI<DataGameProgressProvider>());

            IInput input = new InputKeyboard();
            _diServices.RegisterSingle<IInput>(input);
            _coroutineRuuner.StartCoroutine(LoopInput(input));
            
            Inject(_objectToInjectDI);
        }

        private void Inject(List<object> _objectToInjectDI)
        {
            foreach (var @object in _objectToInjectDI) _diServices.InjectSingle(@object);
        }

        private T CreateDI<T>() where T : class, new()
        {
            T result = new T();
            _diServices.RegisterSingle<T>(result);
            return result;
        }

        private IEnumerator LoopInput(IInput input)
        {
            while (true)
            {
                input.Update();
                yield return null;
            }
        }
    }
}