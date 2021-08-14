using UnityEngine;

namespace HabObjects.Actors.Component.Enemy.Bat
{
    public class ReciverGoblinAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Actor _actor;

        private void AttackMomentInAnimation() => _actor.BloodSystem.Fire(new GoblinAttackMoment());
    }
}