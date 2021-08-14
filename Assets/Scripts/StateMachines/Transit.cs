using UnityEngine;

namespace StateMachines.Enemy
{
    public abstract class Transit : MonoBehaviour
    {
        public const string PathRoom = "1 StateMachine/Room/Transit/";
        public const string PathEnemy = "1 StateMachine/Enemy/Transit/";

        [SerializeField] protected State _target;
        public State Target => _target;
        
        public abstract bool CanTransit();
    }
}