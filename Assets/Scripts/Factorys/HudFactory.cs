using System;
using System.Collections.Generic;
using Huds;
using Plugins.HabObject.DIContainer;
using UnityEngine;

namespace Factorys
{
    public class HudFactory : MonoBehaviour
    {
        public const string IdPlayerCanvas = "PlayeCanvas";

        [SerializeField] private Hud[] _prefabs = new Hud[0];
        
        private DiServices _main = DiServices.MainContainer;

        public Hud Crete<T>() where T : Hud
        {
            Hud template = GetHud<T>();
            template.gameObject.SetActive(false);
            Hud hud = Instantiate(template);
            template.gameObject.SetActive(true);
            if(typeof(T) == typeof(PlayerHud))
                _main.RegisterSingle<Canvas>(hud.GetComponent<Canvas>(), IdPlayerCanvas);
            _main.InjectSingle(hud.gameObject);
            hud.gameObject.SetActive(true);
            return hud;
        }

        private Hud GetHud<T>() where T : Hud
        {
            foreach (var prefab in _prefabs)
            {
                if (prefab.GetType() == typeof(T))
                    return prefab;
            }
            foreach (var prefab in _prefabs)
            {
                if (prefab is T)
                    return prefab;
            }
            throw null;
        }

        
        
    }
}