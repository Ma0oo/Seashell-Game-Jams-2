using UnityEngine;

namespace HabObjects.Items.Components
{
    public class ArrowReadyToFire
    {
        public PointForArrow PointForArrow { get; }
        public Transform TargetRotation { get; }

        public ArrowReadyToFire(PointForArrow pointForArrow, Transform targetRotation)
        {
            PointForArrow = pointForArrow;
            TargetRotation = targetRotation;
        }
    }
}