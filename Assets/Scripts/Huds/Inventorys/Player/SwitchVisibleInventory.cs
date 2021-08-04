using Plugins.HabObject.DIContainer;
using Services.Interfaces;
using UnityEngine;

namespace Huds.Inventorys.Player
{
    public class SwitchVisibleInventory : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private IInput _input;

        private void Awake()
        {
            _input = DiServices.MainContainer.ResolveSingle<IInput>();
            _input.InventoryButton += OnClickInventoryButton;
        }

        private void Start() => ChangeVisible(false);

        private void OnClickInventoryButton() => ChangeVisible(!_canvasGroup.interactable);

        private void ChangeVisible(bool state)
        {
            _canvasGroup.interactable = state;
            _canvasGroup.alpha = ConvertToFloat(state);
            gameObject.SetActive(state);
        }

        private static float ConvertToFloat(bool canvasGroupInteractable) => canvasGroupInteractable ? 1 : 0;
    }
}