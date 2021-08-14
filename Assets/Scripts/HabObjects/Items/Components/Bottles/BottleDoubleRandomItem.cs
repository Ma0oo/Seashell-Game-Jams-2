using System;
using System.Collections.Generic;
using Factorys;
using HabObjects.Actors.Component;
using HabObjects.Items.Signals;
using Plugins.HabObject.Customizable.Attributs;
using Plugins.HabObject.DIContainer;
using Services.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HabObjects.Items.Components.Bottles
{
    [CustomizableComponent("Зелье для дубликата предмета из инвентаря", 0)]
    public class BottleDoubleRandomItem : BottleEffectAbs
    {
        public override string Description => _description;

        [SerializeField] [TString("Описание действия зелья", 50)]private string _description;
        [SerializeField] private Item _item;
        
        private Actor _owner;

        [DI] private IInput _input;
        [DI] private FactoryItem _factoryItem;

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
            var items = _owner.ComponentShell.Get<Inventory>().GetItem(x => true);
            if(items.Count==0)
                return;

            DublicateItem(items);
            DestroySelf();
        }

        private void DestroySelf()
        {
            if(_owner.ComponentShell.Get<InventoryHand>().TryRemove(_item))
                Destroy(_item.gameObject);
        }

        private void DublicateItem(List<Item> items)
        {
            var newItem = _factoryItem.DublicateByPath(items[Random.Range(0, items.Count)]);
            newItem.gameObject.SetActive(true);
            newItem.transform.position = _owner.transform.position;
        }
    }
}