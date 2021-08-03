using HabObjects.Actors.Signals;
using Mechanics.Interfaces;
using UnityEngine;

namespace HabObjects.Actors.Component.Player
{
    public class SenderInterectSignal : MonoBehaviour
    {
        [SerializeField] private Actor _actor;

        public void Send(IInteractbleComponent interactbleComponent)
        {
            if (interactbleComponent == null) throw null;
            interactbleComponent.HabObject.BloodSystem.Fire(new Interact(_actor));
        }
    }
}