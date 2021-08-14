using System;
using System.Collections;
using HabObjects.Items.Data;
using PhysicShell;
using Plugins.HabObject.Customizable.Attributs;
using UnityEngine;

namespace HabObjects.Items.Components
{
    [CustomizableComponent("Компонент стрелы", 0)]
    public class ArrowControl : MonoBehaviour
    {
        [SerializeField] private Item _parentItem;

        [SerializeField] private Vector3 _offsetRotate;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _mainCollider;
        [SerializeField] private ColliderShell _colliderAttack;
        [TRangeFloat("Скорость полета", 0, 500, new float[]{1,5,10})]
        [Min(0.1f)][SerializeField] private float _speedFly;

        private Coroutine _rotateAction;
        private Coroutine _moveArrow;
        
        private void Awake()
        {
            if (!_parentItem.GeneralContainer.Has<ArrowFlag>())
                throw null;
            _parentItem.BloodSystem.Track<ArrowReadyToFire>(OnReady);
            _parentItem.BloodSystem.Track<FireArrow>(OnFire);
            _parentItem.BloodSystem.Track<ArrowHited>(OnHited);
        }

        private void OnHited(ArrowHited obj)
        {
            _colliderAttack.enabled = false;
            _mainCollider.enabled = true;
            StopCoroutine(_moveArrow);
        }

        private void OnReady(ArrowReadyToFire obj)
        {
            _rigidbody2D.simulated = false;
            _mainCollider.enabled = false;
            _colliderAttack.enabled = false;
            _rotateAction = StartCoroutine(Rotate(obj));
        }

        private void OnFire(FireArrow obj)
        {
            StopCoroutine(_rotateAction);
            _rigidbody2D.simulated = true;
            _mainCollider.enabled = false;
            _colliderAttack.enabled = true;
            _moveArrow = StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            Vector3 dir = _parentItem.GeneralContainer.GetOrNull<DiractionArrow>().Diraction;
            while (true)
            {
                _rigidbody2D.MovePosition(_parentItem.transform.position + (dir * (_speedFly * Time.deltaTime)));
                yield return new WaitForFixedUpdate();
            }
        }
        
        private IEnumerator Rotate(ArrowReadyToFire e)
        {
            while (true)
            {
                _parentItem.transform.position = e.PointForArrow.Value.position;
                _parentItem.transform.rotation = Quaternion.Euler( e.TargetRotation.eulerAngles + _offsetRotate);
                yield return null;
            }
        }
    }
}