using System;
using HabObjects.Actors.Signals;
using Mechanics.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace HabObjects.Items.Components
{
    public class ReceiverSignalInteractSelect : MonoBehaviour
    {
        [SerializeField] private HabObject _parent;
        [SerializeField] private UnityEvent _onSelect;
        [SerializeField] private UnityEvent _onUnselect;

        private void OnEnable() => _parent.BloodSystem.Track<SelectToInteract>(OnSignal);

        private void OnDisable() => _parent.BloodSystem.Untrack<SelectToInteract>(OnSignal);

        private void OnSignal(SelectToInteract obj)
        {
            if(obj.ToActive) _onSelect?.Invoke();
            else _onUnselect?.Invoke();
        }
    }
}