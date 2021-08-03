using HabObjects.Actors.Signals;
using HabObjects.Items.Signals;
using UnityEngine;

namespace HabObjects.Actors.Component.Player
{
    public class DropItemByRigibody : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        
        private void Awake()
        {
            _actor.BloodSystem.Track<DropThisItem>(OnDropItem);
        }

        private void OnDropItem(DropThisItem @event)
        {
            @event.Item.transform.position = _actor.transform.position;
            @event.Item.BloodSystem.Fire(new Droped(_actor));
        }
    }
}