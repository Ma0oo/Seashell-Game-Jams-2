using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Configs
{
    [CreateAssetMenu]
    public class SceneSetConfig : ScriptableObject
    {
        public string MainMenu;
        public string Lobby;
        public string Game;
    }
    
}