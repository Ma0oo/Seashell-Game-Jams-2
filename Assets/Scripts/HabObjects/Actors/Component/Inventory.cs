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
    public class Inventory : MonoBehaviour, IInventory, ISaveDataPlayer
    {
        public int MaxItem => _maxItem;
        
        [SerializeField] private Actor _actor;
        [Min(0)] [SerializeField] private int _maxItem;

        private List<Item> _items = new List<Item>();

        private void Awake()
        {
            _actor.BloodSystem.Track<ManualUpdateInventory>(OnManulUpdate);
        }

        private void OnManulUpdate(ManualUpdateInventory obj) => CallEventUpdate();

        public bool TryAdd(Item item)
        {
            if (_items.Count < _maxItem && !_items.Contains(item))
            {
                _items.Add(item);
                item.BloodSystem.Fire(new Picked(_actor));
                CallEventUpdate();
                return true;
            }
            return false;
        }

        public bool TryRemove(Item item)
        {
            var result = _items.Remove(item);
            if (result) _actor.BloodSystem.Fire(new DropThisItem(item));
            CallEventUpdate();
            return result;
        }

        private void CallEventUpdate() => _actor.BloodSystem.Fire(new InventoryUpdate(_items, this.GetType()));

        public List<Item> GetItem(Func<Item, bool> conditions)
        {
            List<Item> result = new List<Item>();
            foreach (var item in _items)
            {
                if(conditions.Invoke(item))
                    result.Add(item);
            }

            return result;
        }
        
        public void Save(DataPlayer data)
        {
            string[] itemsPath = new string[_items.Count];
            for (int i = 0; i < _items.Count; i++) itemsPath[i] = _items[i].GeneralContainer.GetOrNull<PathToPrefab>().Path;
            data.PathItemsPrefab = data.PathItemsPrefab.ConnectArray(itemsPath);
        }

        public void Load(DataPlayer data)
        {
        }
    }
}