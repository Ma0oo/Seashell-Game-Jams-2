using HubObject.Actors.Signals.Bat;
using UnityEngine;

namespace HubObject.Actors.Component.Enemy.Bat
{
    public class ReciverBatAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Actor _actor;

        private void AttackMomentInAnimation() => _actor.BloodSystem.Fire(new BatAttackMoment());
    }
}