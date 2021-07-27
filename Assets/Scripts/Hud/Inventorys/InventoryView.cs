using System;
using System.Collections.Generic;
using HubObject;
using HubObject.Actors.Component;
using HubObject.Actors.Component.Interfaces;
using HubObject.Actors.Signals;
using UnityEngine;

namespace Hud.Inventorys
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        public Type TypeInventoryForView => typeof(Inventory);
        
        [SerializeField] private Actor _actor;
        [SerializeField] private InventoryCell _cellTemplate;
        [SerializeField] private Transform _content;

        private List<GameObject> _objectCell;

        private void Awake() => _actor.BloodSystem.Track<InventoryUpdate>(OnInventoryUpdate);

        private void OnInventoryUpdate(InventoryUpdate @event)
        {
            if (@event.TypeInventory == typeof(Inventory))
            {
                DestroyPrevItem();
                CreateArrayObject(@event);
                SpawnCell(@event);
            }
        }

        private void CreateArrayObject(InventoryUpdate @event) => _objectCell = new List<GameObject>();

        private void SpawnCell(InventoryUpdate @event)
        {
            for (int i = 0; i < @event.Items.Count; i++)
            {
                InventoryCell cell = Instantiate(_cellTemplate, _content);
                cell.Init(@event.Items[i], transform);
                _objectCell.Add(cell.gameObject);
                cell.Droped += OnDrop;
                cell.TransitTo += OnTransit;
            }
        }

        private void DestroyPrevItem()
        {
            if (_objectCell != null)
                foreach (var cell in _objectCell)
                {
                    cell.GetComponent<InventoryCell>().Droped -= OnDrop;
                    cell.GetComponent<InventoryCell>().TransitTo -= OnTransit;
                    Destroy(cell);
                }
        }


        private void OnDrop(Item item) => _actor.ComponentShell.Get<Inventory>().TryRemove(item);

        private void OnTransit(Type typeNewInventory, Item item)
        {
            OnDrop(item);
            (_actor.ComponentShell.Get(typeNewInventory) as IInventory).TryAdd(item);
        }
    }
}