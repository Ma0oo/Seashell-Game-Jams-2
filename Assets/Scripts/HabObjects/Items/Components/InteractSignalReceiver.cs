using System;
using HabObjects.Actors.Signals;
using Mechanics.Interfaces;
using Plugins.HabObject;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HabObjects.Items.Components
{
    public class InteractSignalReceiver : MonoBehaviour, IInteractbleComponent
    {
        public HabObject HabObject => _parent;
        public bool IsActive => enabled;
        
        [SerializeField] private HabObject _parent;
        [SerializeField] private UnityEvent _onInteract;


        private void OnEnable() => _parent.BloodSystem.Track<Interact>(OnInteract);

        private void OnDisable() => _parent.BloodSystem.Untrack<Interact>(OnInteract);

        private void OnInteract(Interact obj) => _onInteract.Invoke();
    }
}