using System;
using Factorys;
using Huds;
using Plugins.HabObject.DIContainer;
using Services.Interfaces;
using UnityEngine;

namespace Mechanics
{
    public abstract class EscControlMenu : MonoBehaviour
    {
        [DI] protected IInput _input;
        [DI] protected HudFactory _hudFactory;

        private MenuEsc _menu;
        
        private void OnEnable() => _input.MenuButtonClick += OnClickMenu;

        private void OnDisable() => _input.MenuButtonClick -= OnClickMenu;

        private void OnClickMenu()
        {
            if (!_menu)
            {
                _menu = (MenuEsc) _hudFactory.Crete<MenuEsc>();
                _menu.gameObject.SetActive(false);
            }

            _menu.gameObject.SetActive(!_menu.gameObject.activeSelf);
        }
    }
}