using System;
using HabObjects.Items.Signals;
using PhysicShell;
using UnityEngine;

namespace HabObjects.Items.Components
{
    public class ArrowAttack : MonoBehaviour
    {
        [SerializeField] private Item _parentItem;
        [SerializeField] private ColliderShell _colliderAttack;

        private void Awake() => _parentItem.BloodSystem.Track<FireArrow>(OnFire);

        private void OnFire(FireArrow obj) => _colliderAttack.Enter += OnEnterColliderAttack;

        private void OnEnterColliderAttack(Collision2D obj)
        {
            _parentItem.BloodSystem.Fire(new ArrowHited(obj.collider));
            _colliderAttack.Enter -= OnEnterColliderAttack;
            if (obj.collider.TryGetComponent<Actor>(out var actor))
                _parentItem.BloodSystem.Fire(new HitedSomeActor(actor));
        }
    }
}