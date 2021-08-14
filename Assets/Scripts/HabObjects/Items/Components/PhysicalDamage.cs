using HabObjects.Actors.Signals;
using HabObjects.Items.Signals;
using Plugins.HabObject;
using Plugins.HabObject.Customizable.Attributs;
using UnityEngine;

namespace HabObjects.Items.Components
{
    [CustomizableComponent("Компоненты физического дамага", 0)]
    public class PhysicalDamage : MonoBehaviour
    {
        [SerializeField] private HabObject _item;
        [TRangeInt("Физический урон", 0, 5000, new int[]{1,5,10,50,100})]
        [SerializeField] private float _damageValue;

        private void Awake() => _item.BloodSystem.Track<HitedSomeActor>(OnHitedSomeActor);

        private void OnHitedSomeActor(HitedSomeActor @event)
        {
            @event.HitedActor.BloodSystem.Fire(new Damaged(_damageValue));
        }
    }
}