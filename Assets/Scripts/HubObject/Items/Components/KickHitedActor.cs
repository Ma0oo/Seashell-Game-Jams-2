using System;
using HubObject.Items.Signals;
using UnityEngine;

namespace HubObject.Items.Components
{
    public class KickHitedActor : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [Min(0)][SerializeField] private float _force;

        private Actor _hosterItem;

        private void Awake()
        {
            _item.BloodSystem.Track<HitedSomeActor>(OnHitedSomeActor);
            _item.BloodSystem.Track<Picked>(OnPicked);
            _item.BloodSystem.Track<Droped>(OnDroped);
            enabled = false;
        }

        private void OnDroped(Droped e)
        {
            enabled = false;
        }

        private void OnPicked(Picked e)
        {
            enabled = true;
            _hosterItem = e.HosterItme;
        }

        private void OnHitedSomeActor(HitedSomeActor e)
        {
            Vector3 direction = e.HitedActor.transform.position - _hosterItem.transform.position;
            direction = direction.normalized;
            e.HitedActor.ComponentShell.Get<Rigidbody2D>().AddForce(direction*_force, ForceMode2D.Impulse);
        }
    }
}