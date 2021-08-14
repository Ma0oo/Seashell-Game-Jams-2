using System;
using System.Collections.Generic;
using Extension;
using HabObjects.Actors.Component.Interfaces;
using HabObjects.Actors.Signals;
using HabObjects.Items.Data;
using HabObjects.Items.Signals;
using Infrastructure.Data;
using Infrastructure.Data.Interface;
using UnityEngine;

namespace HabObjects.Actors.Component
{
    public class InventoryHand : MonoBehaviour, IInventory, ISaveDataPlayer
    {
        public Item ItemInHand => _currentItem;
        
        [SerializeField] private Actor _actor;
        
        private Item _currentItem;

        private void Awake()
        {
            _actor.BloodSystem.Track<ActorHasDead>(OnDeadActor);
            //_actor.BloodSystem.Track<ManualUpdateInventory>(OnManulUpdateInvetory);
        }

        public bool TryAdd(Item item)
        {
            if (_currentItem == null)
            {
                _currentItem = item;
                _actor.BloodSystem.Fire(new InventoryUpdate(new List<Item>(){item}, this.GetType()));
                item.BloodSystem.Fire(new Picked(_actor, item));
                item.BloodSystem.Fire(new ItemInHand());
                return true;
            }
            return false;
        }

        public bool TryRemove(Item item)
        {
            if (_currentItem == item && item != null)
            {
                _currentItem = null;
                _actor.BloodSystem.Fire(new InventoryUpdate(new List<Item>(){}, this.GetType()));
                _actor.BloodSystem.Fire(new DropThisItem(item));
                item.BloodSystem.Fire(new ItemRemoveFromHand());
                return true;
            }
            return false;
        }

        private void OnManulUpdateInvetory(ManualUpdateInventory obj)
        {
            if(_currentItem)
                _actor.BloodSystem.Fire(new InventoryUpdate(new List<Item>() {_currentItem }, this.GetType()));
            else
                _actor.BloodSystem.Fire(new InventoryUpdate(new List<Item>() { }, this.GetType()));
        }

        private void OnDeadActor(ActorHasDead obj) => TryRemove(_currentItem);
        
        public void Save(DataPlayer data)
        {
            if(!_currentItem)
                return;

            data.PathItemsPrefab = data.PathItemsPrefab.ConnectArray(new string[] {_currentItem.GeneralContainer.GetOrNull<PathToPrefab>().Path});
        }

        public void Load(DataPlayer data)
        {
        }
    }
}