using HabObjects;
using UnityEngine;

namespace Factorys
{
    [CreateAssetMenu]
    public class DataEnemy : ScriptableObject
    {
        public Actor Template;
        public Difficulty DifficultyEnemy;

        public enum Difficulty 
        {
            Easy, Med, Hard
        }
    }
}