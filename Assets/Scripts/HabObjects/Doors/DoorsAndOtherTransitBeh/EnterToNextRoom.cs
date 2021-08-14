using System;
using HabObjects.Actors.Signals;
using Mechanics.Interfaces;
using Plugins.HabObject;
using Plugins.HabObject.DIContainer;
using TMPro;
using UnityEngine;

namespace HabObjects.Actors.Component.DoorsAndOtherTransitBeh
{
    public class EnterToNextRoom : MonoBehaviour, IInteractbleComponent
    {
        [SerializeField] private HabObject _parentHab;
        [SerializeField] private TextMeshPro _label;
        [SerializeField] private Collider2D _colliderInterect;
        
        [DI] private Dungeon _dungeon;
        public HabObject HabObject => _parentHab;
        public bool IsActive => enabled;

        private void Awake()
        {
            _label.enabled = false;
            _parentHab.BloodSystem.Track<SelectToInteract>(OnSelect);
            _parentHab.BloodSystem.Track<Interact>(OnInteract);
        }

        private void OnInteract(Interact obj)
        {
            enabled = false;
            _colliderInterect.enabled = false;
            _dungeon.BloodSystem.Fire(new TransitPlayerToNewRoom());
        }

        private void OnSelect(SelectToInteract obj) => _label.enabled = obj.ToActive;
    }
}