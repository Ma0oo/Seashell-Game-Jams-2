using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu]
    public class ConfigPrefabs : ScriptableObject
    {
        [Min(5)]public int StartSizePoolAudio;
        public string PathToMoneyPrefabFolder;
        public string PathToBottleItem;
        public string PathToAllItem;
        public string PathToWeaponItem;
        public AudioSource SourcePrefab => _sourcePrefab;

        [SerializeField] private AudioSource _sourcePrefab;
    }
}