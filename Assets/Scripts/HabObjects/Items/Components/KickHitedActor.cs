using HabObjects.Actors.Data;
using HabObjects.Items.Signals;
using Plugins.HabObject.Customizable.Attributs;
using UnityEngine;

namespace HabObjects.Items.Components
{
    [CustomizableComponent("Компоненты откидывания цели", 0)]
    public class KickHitedActor : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [TRangeFloat("Сила откидвывания", 0, 500, new float[]{1,5,10,50,100})]
        [Min(0)][SerializeField] private float _force;

        private Actor _hosterItem;

        private void Awake()
        {
            _item.BloodSystem.Track<HitedSomeActor>(OnHitedSomeActor);
            _item.BloodSystem.Track<Picked>(OnPicked);
            _item.BloodSystem.Track<Droped>(OnDroped);
            enabled = false;
        }

        private void OnDroped(Droped e)
        {
            enabled = false;
        }

        private void OnPicked(Picked e)
        {
            enabled = true;
            _hosterItem = e.HosterItme;
        }

        private void OnHitedSomeActor(HitedSomeActor e)
        {
            if(!e.HitedActor.GeneralContainer.GetOrNull<DungeonMonsterFlag>())
                return;
            Vector3 direction = e.HitedActor.transform.position - _hosterItem.transform.position;
            direction = direction.normalized;
            e.HitedActor.ComponentShell.Get<Rigidbody2D>()?.AddForce(direction*_force, ForceMode2D.Impulse);
        }
    }
}