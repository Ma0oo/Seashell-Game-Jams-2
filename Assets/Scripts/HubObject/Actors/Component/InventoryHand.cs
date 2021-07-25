using System.Collections.Generic;
using HubObject.Actors.Component.Interfaces;
using HubObject.Actors.Signals;
using HubObject.Items.Signals;
using UnityEngine;

namespace HubObject.Actors.Component
{
    public class InventoryHand : MonoBehaviour, IInventory
    {
        [SerializeField] private Actor _actor;
        
        private Item _currentItem;
        
        public bool TryAdd(Item item)
        {
            if (_currentItem == null)
            {
                _currentItem = item;
                _actor.BloodSystem.Fire(new InventoryUpdate(new List<Item>(){item}, this.GetType()));
                item.BloodSystem.Fire(new Picked(_actor));
                item.BloodSystem.Fire(new ItemInHand());
                return true;
            }
            return false;
        }

        public bool TryRemove(Item item)
        {
            if (_currentItem == item)
            {
                _currentItem = null;
                _actor.BloodSystem.Fire(new InventoryUpdate(new List<Item>(){}, this.GetType()));
                _actor.BloodSystem.Fire(new DropThisItem(item));
                item.BloodSystem.Fire(new ItemRemoveFromHand());
                return true;
            }
            return false;
        }
    }
}