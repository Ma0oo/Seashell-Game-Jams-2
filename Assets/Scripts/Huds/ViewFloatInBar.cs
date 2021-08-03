using System;
using HabObjects;
using HabObjects.Actors.Signals;
using Huds.Inventorys;
using UnityEngine;
using UnityEngine.UI;

namespace Huds
{
    public class ViewFloatInBar : MonoBehaviour, IActorInit
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private ViewThing _targer;
        [SerializeField] private Image _image;

        public void Init(Actor parentActor)
        {
            if(_actor!=null)
                UnSubscribe(_targer);
            _actor = parentActor;
            Subscribe(_targer);
        }

        private void Subscribe(ViewThing targer)
        {
            switch (targer)
            {
                case ViewThing.Health:
                    _actor.BloodSystem.Track<HealthUpdated>(OnHealthUpdate);
                    _actor.BloodSystem.Fire(new ManualUpdateHealth());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targer), targer, null);
            }
        }

        private void UnSubscribe(ViewThing target)
        {
            switch (target)
            {
                case ViewThing.Health:
                    _actor.BloodSystem.Untrack<HealthUpdated>(OnHealthUpdate);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }
        }

        private void OnHealthUpdate(HealthUpdated @event) => _image.fillAmount = @event.Current/@event.Max;


        private enum ViewThing
        {
            Health
        }
    }
}