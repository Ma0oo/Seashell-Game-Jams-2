using System;
using HabObjects.Actors.Component.DoorsAndOtherTransitBeh;
using HabObjects.Actors.Component.Interfaces;
using HabObjects.Actors.Signals;
using Infrastructure.Data;
using Plugins.HabObject.Customizable.Attributs;
using UnityEngine;

namespace HabObjects.Actors.Component
{
    [CustomizableComponent("Настройки здоровья", 0)]
    public class HealthMaxInInside : HealthAbs
    {
        [SerializeField] private Actor _actor;
        [TRangeFloat("Max hp", 0, 1000, new[] {1,5, 10f, 15})]
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            _actor.BloodSystem.Track<ManualUpdateHealth>(@event => CallEventUpdate());
        }

        private void OnEnable() => CallEventUpdate();

        public override float Current => _currentHealth;

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
            data.HealthMax = _maxHealth;
            data.Health = _currentHealth;
        }

        public override void Load(DataPlayer data)
        {
            _maxHealth = data.HealthMax;
            _currentHealth = data.Health;
            CallEventUpdate();
        }

        private void CallEventUpdate() => _actor.BloodSystem.Fire(new HealthUpdated(_currentHealth, _maxHealth));

        private void Clamp() => _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }
}