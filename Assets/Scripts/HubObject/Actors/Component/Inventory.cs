using System.Collections.Generic;
using HubObject.Actors.Component.Interfaces;
using HubObject.Actors.Signals;
using HubObject.Items.Signals;
using UnityEngine;

namespace HubObject.Actors.Component
{
    public class Inventory : MonoBehaviour, IInventory
    {
        [SerializeField] private Actor _actor;
        [Min(0)] [SerializeField] private int _maxItem;

        private List<Item> _items = new List<Item>();

        public bool TryAdd(Item item)
        {
            if (_items.Count < _maxItem && !_items.Contains(item))
            {
                _items.Add(item);
                item.BloodSystem.Fire(new Picked(_actor));
                _actor.BloodSystem.Fire(new InventoryUpdate(_items, this.GetType()));
                return true;
            }
            return false;
        }
        
        public bool TryRemove(Item item)
        {
            var result = _items.Remove(item);
            _actor.BloodSystem.Fire(new InventoryUpdate(_items, this.GetType()));
            if (result) _actor.BloodSystem.Fire(new DropThisItem(item));
            return result;
        }
    }
}