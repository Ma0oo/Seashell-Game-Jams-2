using System;
using Plugins.HubObject.GlobalSystem;
using Services.Interfaces;
using UnityEngine;

namespace Hud.Inventorys
{
    public class SwitchVisibleInventory : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private IInput _input;

        private void Awake() => _input = ServicesLocator.MainContainer.ResolveSingle<IInput>();

        private void OnEnable() => _input.InventoryButton += OnClickInventoryButton;

        private void OnDisable() => _input.InventoryButton -= OnClickInventoryButton;

        private void Start() => ChangeVisible(false);

        private void OnClickInventoryButton() => ChangeVisible(!_canvasGroup.interactable);

        private void ChangeVisible(bool state)
        {
            _canvasGroup.interactable = state;
            _canvasGroup.alpha = ConvertToFloat(state);
        }

        private static float ConvertToFloat(bool canvasGroupInteractable) => canvasGroupInteractable ? 1 : 0;
    }
}