using UnityEngine;

namespace StateMachines.Enemy
{
    public abstract class Transit : MonoBehaviour
    {
        public abstract bool CanTransit();
        public abstract EnemyState Target { get; }
    }
}