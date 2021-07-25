using System;
using System.Collections;
using HubObject.Items.Signals;
using UnityEngine;

namespace HubObject.Items.Components
{
    public class SwitcherOnPickDrop : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private Collider2D _colliderBody;

        private void OnEnable()
        {
            _item.BloodSystem.Track<Picked>(OnPick);
            _item.BloodSystem.Track<Droped>(OnDrop);
        }

        private void OnPick(Picked @event)
        {
            _colliderBody.enabled = false;
            _item.gameObject.SetActive(false);
            _item.transform.position = new Vector2(1000,1000);
        }

        private void OnDrop(Droped @event)
        {
            _colliderBody.enabled = true;
            _item.gameObject.SetActive(true);
        }
    }
}