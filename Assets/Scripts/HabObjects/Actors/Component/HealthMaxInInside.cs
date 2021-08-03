using HabObjects.Actors.Component.Interfaces;
using HabObjects.Actors.Signals;
using Infrastructure.Data;
using UnityEngine;

namespace HabObjects.Actors.Component
{
    public class HealthMaxInInside : HealthAbs
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _actor.BloodSystem.Track<ManualUpdateHealth>(@event => CallEventUpdate());
        }

        public override void Damage(float damage)
        {
            if(!enabled) return;
            _currentHealth -= damage;
            Clamp();
            CallEventUpdate();
        }

        public override void Recovery(float recovery)
        {
            if(!enabled) return;
            _currentHealth += recovery;
            Clamp();
            CallEventUpdate();
        }

        public override void Save(DataPlayer data)
        {
            data.Health = _currentHealth;
            data.HealthMax = _maxHealth;
        }

        public override void Load(DataPlayer data)
        {
            _currentHealth = data.Health;
            _maxHealth = data.HealthMax;
            CallEventUpdate();
        }

        private void CallEventUpdate() => _actor.BloodSystem.Fire(new HealthUpdated(_currentHealth, _maxHealth));

        private void Clamp() => _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }
}