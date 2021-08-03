using System;
using HabObjects;
using HabObjects.Actors.Data;
using TMPro;
using UnityEngine;

namespace Huds.Inventorys.Player
{
    public class MoneyView : MonoBehaviour, IActorInit
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private TextMeshProUGUI _label;

        private void Awake() => Subscribe();

        public void Init(Actor parentActor)
        {
            UnSubscribe();
            _actor = parentActor;
            Subscribe();
            _actor.BloodSystem.Fire(new ManualUpdateMoney());
        }

        private void Subscribe()
        {
            if(_actor)
                _actor.BloodSystem.Track<MoneyUpdate>(OnUpdate);
        }

        private void UnSubscribe()
        {
            if (_actor)
                _actor.BloodSystem.Untrack<MoneyUpdate>(OnUpdate);
        }

        private void OnUpdate(MoneyUpdate obj) => _label.text = obj.NewValue.ToString();
    }
}