using System;
using HabObjects.Actors.Component;
using HabObjects.Actors.Component.Interfaces;
using HabObjects.Items.Signals;
using Plugins.HabObject.Customizable.Attributs;
using Plugins.HabObject.DIContainer;
using Services.Interfaces;
using UnityEngine;

namespace HabObjects.Items.Components.Bottles
{
    [CustomizableComponent("Зелье с токсинами", 0)]
    public class BottleToxin : BottleEffectAbs
    {
        public override string Description => _description + _hpToDamage.ToString(); 

        [SerializeField] private Item _item;
        
        [SerializeField] [TString("Описание действия", 35)]
        private string _description;
        
        [SerializeField] [Range(0,100)] [TRangeFloat("колличество урона здоровью", 0, 100, new [] {1f, 5, 10})]
        private float _hpToDamage;

        [DI] private IInput _input;

        private Actor _owner;

        private void Awake()
        {
            enabled = false;
            _item.BloodSystem.Track<ItemInHand>(x=> _input.MainAttackClick += OnAttackClick);
            _item.BloodSystem.Track<ItemRemoveFromHand>(x => _input.MainAttackClick -= OnAttackClick);
            _item.BloodSystem.Track<Picked>(x => _owner = x.HosterItme);
            _item.BloodSystem.Track<Droped>(x =>
            {
                if (x.PrevHostItme == _owner)
                    _owner = null;
                else
                    throw new Exception("Дроп произошел не от владельца");
            });
        }

        private void OnAttackClick()
        {
            _owner.ComponentShell.Get<HealthAbs>().Damage(_hpToDamage);
            if(_owner.ComponentShell.Get<InventoryHand>().TryRemove(_item))
                Destroy(_item.gameObject);
        }
    }
}