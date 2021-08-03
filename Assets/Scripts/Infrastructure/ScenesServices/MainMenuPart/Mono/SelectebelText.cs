using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.ScenesServices.MainMenuPart.Mono
{
    public class SelectebelText : MonoBehaviour, IPointerClickHandler
    {
        public TextMeshProUGUI Label => _label;
        [SerializeField] private TextMeshProUGUI _label;

        public event Action<string> Selected;

        public void OnPointerClick(PointerEventData eventData) => Selected?.Invoke(_label.text);

        public void Init(Color color, string text)
        {
            _label.color = color;
            _label.text = text;
        }
    }
}