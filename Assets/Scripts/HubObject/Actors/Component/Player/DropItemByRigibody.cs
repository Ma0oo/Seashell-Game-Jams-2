using System;
using HubObject.Actors.Signals;
using HubObject.Items.Signals;
using UnityEngine;

namespace HubObject.Actors.Component.Player
{
    public class DropItemByRigibody : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        
        private void Awake()
        {
            _actor.BloodSystem.Track<DropThisItem>(OnDropItem);
        }

        private void OnDropItem(DropThisItem @event)
        {
            @event.Item.transform.position = _actor.transform.position;
            @event.Item.BloodSystem.Fire(new Droped(_actor));
        }
    }
}