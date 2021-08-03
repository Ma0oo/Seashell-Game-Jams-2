using System;
using HabObjects;
using Infrastructure;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Services.Cameras
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [DI(DIConstID.PlayerId)] private Actor _target;
        [SerializeField] [Min(0)] private float _maxDistancing;
        [Min(0)][SerializeField] private float _speed;
        [SerializeField] private Camera _camera;

        private const float OffsetZ = -10;

        private void LateUpdate() => Move(PositionMove());

        private void Move(Vector3 positionMove) => transform.position = Vector3.MoveTowards(transform.position, positionMove, Time.deltaTime * _speed);

        private Vector3 PositionMove()
        {
            Vector3 result = PlayerPositon() + OffsetToCursor();
            result.z = OffsetZ;
            return result;
        }

        private Vector3 OffsetToCursor()
        {
            Vector3 cursotInWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = cursotInWorld - _target.transform.position;
            if (offset.magnitude > _maxDistancing)
                return offset.normalized * _maxDistancing;
            return offset;
        }

        private Vector3 PlayerPositon() => _target.transform.position;
    }
}