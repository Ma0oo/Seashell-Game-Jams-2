using UnityEngine.Audio;

namespace Extension
{
    public static class AudioMixerExtension
    {
        public static AudioMixerGroup GetGroupByName(this AudioMixer mixer, string name)
        {
            var groups = mixer.FindMatchingGroups("Master");
            foreach (var group in groups)
            {
                if (group.name == name)
                    return group;
            }

            throw null;
        }

        public static string[] ConnectArray(this string[] parent, string[] other)
        {
            string[] result = new string[parent.Length+other.Length];
            for (int i = 0; i < parent.Length+other.Length; i++)
            {
                if (i < parent.Length)
                    result[i] = parent[i];
                else
                    result[i] = other[i - parent.Length];
            }

            return result;
        }
    }
}