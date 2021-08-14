using System;
using UnityEngine;

namespace PhysicShell
{
    public class TriggerShell : MonoBehaviour
    {
         public Collider2D Collider2D =>  _collider2D;

         [SerializeField]private Collider2D _collider2D; 
        
        public event Action<Collider2D> Enter;
        public event Action<Collider2D> Stay;
        public event Action<Collider2D> Exit;

        private void OnEnable() => _collider2D.enabled = true;

        private void OnDisable() => _collider2D.enabled = false;

        private void OnTriggerEnter2D(Collider2D other) => Enter?.Invoke(other);

        private void OnTriggerStay2D(Collider2D other) => Stay?.Invoke(other);

        private void OnTriggerExit2D(Collider2D other) => Exit?.Invoke(other);
    }
}