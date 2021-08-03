using UnityEngine;

namespace Infrastructure.Data
{
    [CreateAssetMenu]
    public class ConfigMixerGroup : ScriptableObject
    {
        public string UI;
        public string Effect;
        public string Music;
    }
}