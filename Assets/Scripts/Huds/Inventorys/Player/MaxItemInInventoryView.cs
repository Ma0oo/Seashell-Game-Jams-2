using System;
using HabObjects;
using HabObjects.Actors.Component;
using HabObjects.Actors.Signals;
using TMPro;
using UnityEngine;

namespace Huds.Inventorys.Player
{
    public class MaxItemInInventoryView : MonoBehaviour, IActorInit
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private TextMeshProUGUI _text;
        
        private Inventory _inventory;
        
        private void Awake() => Init(_actor);

        public void Init(Actor parentActor)
        {
            if(_actor)
                _actor.BloodSystem.Untrack<InventoryUpdate>(OnInventoryUpdate);
            _actor = parentActor;
            
            if (!_actor) return;
            
            _actor.BloodSystem.Track<InventoryUpdate>(OnInventoryUpdate);
            _inventory = _actor.ComponentShell.Get<Inventory>();
            _actor.BloodSystem.Fire(new ManualUpdateInventory());
        }

        private void OnInventoryUpdate(InventoryUpdate obj) => _text.text = $"{obj.Items.Count}/{_inventory.MaxItem}";
    }
}