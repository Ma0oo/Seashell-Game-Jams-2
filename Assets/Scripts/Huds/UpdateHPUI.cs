using System.Collections;
using HabObjects;
using HabObjects.Actors.Signals;
using Huds.Inventorys;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Huds
{
    public class UpdateHPUI : MonoBehaviour, IActorInit
    {
        public void Init(Actor parentActor) => StartCoroutine(UpdateSignal(parentActor));

        private IEnumerator UpdateSignal(Actor actor)
        {
            yield return null;
            actor.BloodSystem.Fire(new ManualUpdateHealth());
        }
    }
}