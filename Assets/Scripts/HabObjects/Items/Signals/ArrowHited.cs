using UnityEngine;

namespace HabObjects.Items.Components
{
    public class ArrowHited
    {
        public Collider2D HitedTarget { get; }

        public ArrowHited(Collider2D hitedTarget) => HitedTarget = hitedTarget;
    }
}