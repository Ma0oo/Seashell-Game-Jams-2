using System;
using HubObject;
using HubObject.Actors.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public class ViewFloatInBar : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private ViewThing _targer;
        [SerializeField] private Image _image;

        private void Awake() => Subscribe(_targer);

        private void Subscribe(ViewThing targer)
        {
            switch (targer)
            {
                case ViewThing.Health:
                    _actor.BloodSystem.Track<HealthUpdated>(e => _image.fillAmount = e.Current/e.Max);
                    _actor.BloodSystem.Fire(new ManualUpdateHealth());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targer), targer, null);
            }
        }


        private enum ViewThing
        {
            Health
        }
    }
}