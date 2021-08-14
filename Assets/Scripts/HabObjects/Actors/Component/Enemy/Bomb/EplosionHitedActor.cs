using System;
using System.Collections.Generic;
using UnityEngine;

namespace HabObjects.Actors.Component.Enemy.Bomb
{
    public class EplosionHitedActor : MonoBehaviour
    {
        [SerializeField] private Actor _actor;
        [Min(0.1f)][SerializeField] private float _radius;

        public List<Actor> GetExplosedHitedActor()
        {
            List<Actor> hitedActors = new List<Actor>();
            foreach (var actors in Physics2D.OverlapCircleAll(_actor.transform.position, _radius))
            {
                if(actors.TryGetComponent<Actor>(out var result))
                    hitedActors.Add(result);
            }
            return hitedActors;
        }        
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(_actor.transform.position, _radius);
        }
    }
}