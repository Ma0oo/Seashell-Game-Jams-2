using System;
using UnityEngine;

namespace PhysicShell
{
    public class ColliderShell : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D; 
        
        public event Action<Collision2D> Enter;
        public event Action<Collision2D> Stay;
        public event Action<Collision2D> Exit;

        private void OnEnable() => _collider2D.enabled = true;

        private void OnDisable() => _collider2D.enabled = false;

        private void OnCollisionEnter2D(Collision2D other) => Enter?.Invoke(other);

        private void OnCollisionStay2D(Collision2D other) => Stay?.Invoke(other);

        private void OnCollisionExit2D(Collision2D other) => Exit?.Invoke(other);
    }
}