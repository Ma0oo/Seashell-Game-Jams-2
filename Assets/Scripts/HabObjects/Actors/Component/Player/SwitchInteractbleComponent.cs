using HabObjects.Actors.Signals;
using Mechanics.Interfaces;
using PhysicShell;
using Plugins.HabObject.DIContainer;
using Services.Interfaces;
using UnityEngine;

namespace HabObjects.Actors.Component.Player
{
    public class SwitchInteractbleComponent : MonoBehaviour
    {
        [SerializeField] private TriggerShell _triggerShell;
        [SerializeField] private PickerUpItem _pickerUpItem;
        [SerializeField] private SenderInterectSignal _senderInterectSignal;

        [DI] private IInput _input;

        private ShellItemAndInterectComponent _shell;

        private void OnEnable()
        {
            _triggerShell.Enter += OnEnter;
            _triggerShell.Exit += OnExit;
            _input.Intractable += OnIntractable;
        }

        private void OnDisable()
        {
            _triggerShell.Enter -= OnEnter;
            _triggerShell.Exit -= OnExit;
            _input.Intractable -= OnIntractable;
        }

        private void OnIntractable()
        {
            if(_shell == null)
                return;
            
            if (_shell.Item)
            {
                _pickerUpItem.PickUp(_shell.Item);
            }
            else if(_shell.InteractbleComponent !=null)
            {
                _senderInterectSignal.Send(_shell.InteractbleComponent);
            }
        }

        private void OnEnter(Collider2D other)
        {
            if (other.TryGetComponent<Mechanics.Interfaces.HabObject>(out var hab))
            {
                if(CheckAtInterface(hab))
                    return;
                if (hab is Item)
                {
                    _shell = new ShellItemAndInterectComponent(hab.gameObject, hab as Item, null);
                    _shell.FireSignalSelectToInteract();
                }
                
            }
        }

        private void OnExit(Collider2D other)
        {
            if(_shell==null) return;
            if (_shell.MainObject == other.gameObject)
            {
                _shell.FireSignalUnselectToInteract();
                _shell = null;
            }
        }
        
        private bool CheckAtInterface(Mechanics.Interfaces.HabObject hab)
        {
            foreach (var component in hab.ComponentShell.GetAll<IInteractbleComponent>())
            {
                if (component.IsActive)
                {
                    _shell = new ShellItemAndInterectComponent(hab.gameObject, null, component);
                    _shell.FireSignalSelectToInteract();
                    return true;
                }
            }
            return false;
        }
        
        private class ShellItemAndInterectComponent
        {
            public GameObject MainObject { get; }
            public Item Item { get; }
            public IInteractbleComponent InteractbleComponent { get; }

            public ShellItemAndInterectComponent(GameObject mainObject, Item item, IInteractbleComponent interactbleComponent)
            {
                MainObject = mainObject;
                Item = item;
                InteractbleComponent = interactbleComponent;
            }

            public void FireSignalSelectToInteract()
            {
                if (Item != null) Item.BloodSystem.Fire(new SelectToInteract(true));
                InteractbleComponent?.HabObject.BloodSystem.Fire(new SelectToInteract(true));
            }

            public void FireSignalUnselectToInteract()
            {
                if (Item != null) Item.BloodSystem.Fire(new SelectToInteract(false));
                InteractbleComponent?.HabObject.BloodSystem.Fire(new SelectToInteract(false));
            }
        }
    }
}