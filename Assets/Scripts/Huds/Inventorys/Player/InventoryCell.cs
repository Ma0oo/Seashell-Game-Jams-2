using System;
using System.Collections.Generic;
using Factorys;
using HabObjects;
using HabObjects.Items.Data;
using Plugins.HabObject.DIContainer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Huds.Inventorys.Player
{
    public class InventoryCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<Type, Item> TransitTo;
        public event Action<Item> Droped;
        
        [SerializeField] private  Image _icon;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Transform _mainPanelInventory;
        
        private Item _item;
        private Canvas _canvas;

        private void Awake() => _canvas = DiServices.MainContainer.ResolveSingle<Canvas>(HudFactory.IdPlayerCanvas);

        public void Init(Item item, Transform mainPanelInventory)
        {
            _item = item;
            _icon.sprite = item.GeneralContainer.GetOrNull<SpriteForInventory>().Value;
            _mainPanelInventory = mainPanelInventory;
        }

        private int _indexSiblingIndex;
        private Transform _lastTransform;
        
        public void OnBeginDrag(PointerEventData eventData)
        { 
            _indexSiblingIndex = transform.GetSiblingIndex();
            _lastTransform = transform.parent;
            transform.SetParent(_canvas.transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(_mainPanelInventory as RectTransform, transform.position))
            {
                transform.SetParent(_lastTransform);
                transform.SetSiblingIndex(_indexSiblingIndex);
            }
            else
            {
                var results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);
                foreach (var result in results)
                {
                    if (result.gameObject.TryGetComponent<IInventoryView>(out var view))
                    {
                        TransitTo?.Invoke(view.TypeInventoryForView, _item);
                        return;
                    }
                }
                Droped?.Invoke(_item);
            }
            
        }
    }
}