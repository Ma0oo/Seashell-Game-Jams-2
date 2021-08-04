using System;
using Plugins.HabObject.DIContainer;
using Services.Inputs;
using Services.Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public class InitTestRoom : ManualBindDi
    {
        private IInput _input;
        
        public override void Create(DiServices container)
        {
            _input = new InputKeyboard();
            DiServices.MainContainer.RegisterSingle<IInput>(_input);
        }

        private void Update() => _input.Update();

        public override void DestroyDi(DiServices container)
        {
        }
    }
}