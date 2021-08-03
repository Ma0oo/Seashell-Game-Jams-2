using UnityEngine;

namespace HabObjects.Actors.Component.Player
{
    public class PickerUpItem : MonoBehaviour
    {
        [SerializeField] private Actor _actor;

        private Inventory _inventory;

        private void Awake() => _inventory = _actor.ComponentShell.Get<Inventory>();

        public bool PickUp(Item item)
        {
            if (!item) throw null;
            return _inventory.TryAdd(item);
        }
    }
}