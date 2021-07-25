using System;
using HubObject.Actors.Data;
using HubObject.Items.Signals;
using UnityEngine;

namespace HubObject.Items.Components
{
    public class RotatedAroundActor : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private float _offsetRotate;
        
        private Actor _actor;
        private PivotCloseWeapon _customPivot;
        private Camera _camera;
        
        private void Awake()
        {
            _item.BloodSystem.Track<Picked>(OnPicked);
            _item.BloodSystem.Track<Droped>(OnDroped);
            _item.BloodSystem.Track<ItemInHand>(OnItemInHand);
            _item.BloodSystem.Track<ItemRemoveFromHand>(OnItemRemoveFromHand);
            enabled = false;
            _camera = Camera.main;
        }
        

        private void OnEnable()
        {
            _customPivot = _actor.GeneralContainer.GetOrNull<PivotCloseWeapon>();
            if(_customPivot==null)
                Debug.LogWarning("This actor don't has pivot for close weapon", _actor);
        }

        private void Update()
        {
            if (_customPivot)
            {
                _item.transform.position = _customPivot.Value.position;
                RotateItem();
            }
            else
            {
                _item.transform.position = _actor.transform.position;
                RotateItem();
            }
        }

        private void RotateItem()
        {
            Vector3 direction = _camera.ScreenToWorldPoint(Input.mousePosition) - _item.transform.position;
            float rotate = (float) (Math.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            _item.transform.rotation = Quaternion.Euler(0, 0, rotate + _offsetRotate);
        }

        private void OnItemRemoveFromHand(ItemRemoveFromHand obj) => enabled = false;

        private void OnItemInHand(ItemInHand obj) => enabled = true;

        private void OnDroped(Droped @event)
        {
            if (_actor == @event.PrevHostItme) _actor = null;
            else Debug.LogError("Предмет не может быть дропнут не хозяином");
        }

        private void OnPicked(Picked @event) => _actor = @event.HosterItme;
    }
}