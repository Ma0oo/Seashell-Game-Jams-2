using System;
using HabObjects.Actors.Data;
using HabObjects.Actors.Signals;
using Mechanics.Interfaces;
using Plugins.HabObject;
using Plugins.HabObject.Customizable.Attributs;
using UnityEngine;

namespace HabObjects.Actors.Component
{
    [CustomizableComponent("компонент монеты", 0)]
    public class MoneyCoin : MonoBehaviour, IInteractbleComponent
    {
        public HabObject HabObject => _parentActor;
        public bool IsActive => enabled;
        public int Value => _moneyToAdd;

        [Min(1)] [SerializeField] [TRangeInt("Деньги для добавление", 0, 1000, new []{1,5,10})] 
        private int _moneyToAdd;
        [SerializeField] private Actor _parentActor;

        private void Awake() => _parentActor.BloodSystem.Track<Interact>(OnInteract);

        private void OnInteract(Interact obj)
        {
            var moneyComponent = obj.Sender.ComponentShell.Get<Money>();
            if(moneyComponent)
                if(moneyComponent.TryChangeAt(_moneyToAdd))
                    Destroy(_parentActor.gameObject);
        }
    }
}