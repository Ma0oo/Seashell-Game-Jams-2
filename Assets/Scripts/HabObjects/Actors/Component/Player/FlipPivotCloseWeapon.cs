using UnityEngine;

namespace HabObjects.Actors.Component.Player
{
    public class FlipPivotCloseWeapon : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private SpriteRenderer _mainSprite;
        [SerializeField] private Transform _customPivot;

        private float _offsetX;

        private void Awake() => _offsetX = (_customPivot.transform.position - _actor.transform.position).x;

        private void Update()
        {
            Vector3 actorVector = _actor.transform.position;
            if (_mainSprite.flipX == false)
                actorVector.x += _offsetX;
            else
                actorVector.x -= _offsetX;
            _customPivot.transform.position = actorVector;
        }
    }
}
