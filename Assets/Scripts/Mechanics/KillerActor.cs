using HabObjects;
using HabObjects.Actors.Signals;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mechanics
{
    public class KillerActor
    {
        public void  InstanceKill(Actor actor) => actor.BloodSystem.Fire(new FinallyDamage(int.MaxValue));
    }
}