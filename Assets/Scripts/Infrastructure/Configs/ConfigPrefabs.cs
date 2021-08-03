using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu]
    public class ConfigPrefabs : ScriptableObject
    {
        [Min(5)]public int StartSizePoolAudio;
        public AudioSource SourcePrefab => _sourcePrefab;
        [SerializeField] private AudioSource _sourcePrefab;
    }
}