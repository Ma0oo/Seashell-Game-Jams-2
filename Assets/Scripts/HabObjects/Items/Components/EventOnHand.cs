using HabObjects.Items.Signals;
using UnityEngine;
using UnityEngine.Events;

namespace HabObjects.Items.Components
{
    public class EventOnHand : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private UnityEvent _onAdd;
        [SerializeField] private UnityEvent _onRemove;

        private void Awake()
        {
            _item.BloodSystem.Track<ItemInHand>(OnHand);
            _item.BloodSystem.Track<ItemRemoveFromHand>(OnRemove);
        }

        private void OnRemove(ItemRemoveFromHand obj) => _onRemove?.Invoke();

        private void OnHand(ItemInHand @event) => _onAdd?.Invoke();
    }
}