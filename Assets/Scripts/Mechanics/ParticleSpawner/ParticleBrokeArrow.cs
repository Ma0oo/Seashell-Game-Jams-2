using HabObjects;
using HabObjects.Items.Components;
using UnityEngine;

namespace Mechanics.ParticleSpawner
{
    public class ParticleBrokeArrow : MonoBehaviour
    {
        [SerializeField] private Item _item;
        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            Vector3 pos = _item.transform.position;
            pos.z = -1;
            _item.BloodSystem.Track<ArrowHasBroken>(x => Instantiate(_particleSystem, pos, Quaternion.identity));
        }
    }
}