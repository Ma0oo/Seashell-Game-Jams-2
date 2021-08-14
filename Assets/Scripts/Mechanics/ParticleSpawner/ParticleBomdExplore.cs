using System;
using HabObjects;
using HabObjects.Actors.Component.Enemy.Bomb;
using UnityEngine;

namespace Mechanics.ParticleSpawner
{
    public class ParticleBomdExplore : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [SerializeField] private Transform _point;
        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake() 
            => _actor.BloodSystem.Track<BombHasExplored>(x=>Instantiate(_particleSystem, _point.transform.position, Quaternion.identity));
    }
}