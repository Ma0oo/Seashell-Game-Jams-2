using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Plugins.HabObject.Customizable.Attributs;
using UnityEngine;

namespace Plugins.HabObject.Customizable.ViewAttribute
{
    public class ViewCustomizableComponent
    {
        private readonly MonoBehaviour _beh;
        private readonly CustomizableComponentAttribute _attribute;

        private List<ViewFieldCustomizableComponent> _viewFields = new List<ViewFieldCustomizableComponent>();

        public ViewCustomizableComponent(MonoBehaviour beh, CustomizableComponentAttribute attribute)
        {
            _beh = beh;
            _attribute = attribute;
        }

        public void GenerateField()
        {
            _viewFields = new List<ViewFieldCustomizableComponent>();
            var fields = GetFieldWithTField();
            
            foreach (var field in fields)
            {
                var fieldView = ViewFieldCustomizableComponent.CreateView(field.GetCustomAttribute<TField>(), field, _beh);
                _viewFields.Add(fieldView);
            }
        }

        public void Render()
        {
            GUILayout.Label($"Component - {_attribute.NameComponent} / GameObject - {_beh.gameObject.name}");
            foreach (var field in _viewFields)
            {
                field.OnGUI();
            }
        }

        private IEnumerable<FieldInfo> GetFieldWithTField()
        {
            return _beh.GetType().
                GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).
                Where(x => x.GetCustomAttribute<TField>() != null);
        }
    }
}