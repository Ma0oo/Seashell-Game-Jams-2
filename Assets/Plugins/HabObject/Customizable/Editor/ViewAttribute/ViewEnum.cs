using System;
using System.Reflection;
using Plugins.HabObject.Customizable.Attributs;
using UnityEditor;
using UnityEngine;

namespace Plugins.HabObject.Customizable.ViewAttribute
{
    public class ViewEnum : ViewFieldCustomizableComponent
    {
        private readonly TEnum _attribute;
        private readonly FieldInfo _fieldInfo;
        private readonly MonoBehaviour _instance;

        public ViewEnum(TEnum attribute, FieldInfo fieldInfo, MonoBehaviour instance)
        {
            _attribute = attribute;
            _fieldInfo = fieldInfo;
            _instance = instance;
        }

        public override void OnGUI()
        {
            GUILayout.Label(_attribute.Name + " / " + _fieldInfo.FieldType.ToString().DeleteBeginCharFromEnd('.'));
            _fieldInfo.SetValue(_instance, EditorGUILayout.EnumPopup((Enum) _fieldInfo.GetValue(_instance)));
        }
    }
}