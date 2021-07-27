using System;
using UnityEngine;

namespace HubObject.Doors.Data
{
    public class DiractionLookDoor : MonoBehaviour
    {
        public Type Diraction => diraction;
        
        [SerializeField] private Type diraction; 
        
        public enum Type
        {
            Up, right, down, left
        }

        public bool IsOppositeByOtherDiraction(DiractionLookDoor otherDiraction)
        {
            switch (otherDiraction.diraction)
            {
                case Type.Up:
                    return diraction == Type.down;
                case Type.right:
                    return diraction == Type.left;
                case Type.down:
                    return diraction == Type.Up;
                case Type.left:
                    return diraction == Type.right;
                default:
                    throw new Exception("Unknow type");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Vector2 diraction = Vector2.zero;
            switch (Diraction)
            {
                case Type.Up:
                    diraction = Vector2.up;
                    break;
                case Type.right:
                    diraction = Vector2.right;
                    break;
                case Type.down:
                    diraction = Vector2.down;
                    break;
                case Type.left:
                    diraction = Vector2.left;
                    break;
            }
            Gizmos.color = Color.yellow;
            var position = (Vector2)transform.position;
            Gizmos.DrawLine(position, position+diraction);
            Gizmos.DrawWireSphere(position+diraction, 0.15f);
        }
    }
}