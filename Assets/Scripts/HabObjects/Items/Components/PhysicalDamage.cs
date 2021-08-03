using HabObjects.Actors.Signals;
using HabObjects.Items.Signals;
using UnityEngine;

namespace HabObjects.Items.Components
{
    public class PhysicalDamage : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private int _damageValue;

        private void Awake() => _item.BloodSystem.Track<HitedSomeActor>(OnHitedSomeActor);

        private void OnHitedSomeActor(HitedSomeActor @event)
        {
            @event.HitedActor.BloodSystem.Fire(new Damaged(_damageValue));
        }
    }
}