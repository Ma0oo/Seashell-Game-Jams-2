using HubObject.Actors.Signals;
using UnityEngine;
using UnityEngine.Events;

namespace HubObject.Actors.Component
{
    public class DeadObserver : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private UnityEvent _onDead;

        private void Awake()
        {
            _actor.BloodSystem.Track<HealthUpdated>(OnUpdateHealth);            
        }

        private void OnUpdateHealth(HealthUpdated obj)
        {
            if (HealthIsEmpty(obj.Current))
            {
                _actor.BloodSystem.Fire(new Dead());
                _onDead?.Invoke();
            }
        }

        private static bool HealthIsEmpty(float current) => current == 0;
    }
}