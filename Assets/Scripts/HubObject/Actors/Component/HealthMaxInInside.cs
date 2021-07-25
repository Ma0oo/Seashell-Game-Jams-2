using HubObject.Actors.Component.Interfaces;
using HubObject.Actors.Signals;
using UnityEngine;

namespace HubObject.Actors.Component
{
    public class HealthMaxInInside : HealthAbs
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private float _maxHealth;

        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _actor.BloodSystem.Track<ManualUpdateHealth>(@event => CallEventUpdate());
        }

        public override void Damage(float damage)
        {
            _currentHealth -= damage;
            Clamp();
            CallEventUpdate();
        }

        public override void Recovery(float recovery)
        {
            _currentHealth += recovery;
            Clamp();
            CallEventUpdate();
        }

        private void CallEventUpdate() => _actor.BloodSystem.Fire(new HealthUpdated(_currentHealth, _maxHealth));

        private void Clamp() => _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }
}