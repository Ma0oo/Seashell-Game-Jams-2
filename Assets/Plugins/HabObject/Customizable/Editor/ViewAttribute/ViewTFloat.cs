using System;
using System.Reflection;
using Plugins.HabObject.Customizable.Attributs;
using UnityEditor;
using UnityEngine;

namespace Plugins.HabObject.Customizable.ViewAttribute
{
    public class ViewTFloat : ViewFieldCustomizableComponent
    {
        private readonly TField _attribute;
        private readonly FieldInfo _fieldInfo;
        private readonly MonoBehaviour _instance;

        public ViewTFloat(TField attribute, FieldInfo fieldInfo, MonoBehaviour instance)
        {
            if(!(attribute is TFloat) && !(attribute is TRangeFloat))
                throw new Exception("Wrong TField");
            _attribute = attribute;
            _fieldInfo = fieldInfo;
            _instance = instance;
        }

        public override void OnGUI()
        {
            if (_attribute is TFloat)
                DrawFloat(_attribute as TFloat);
            else
                DrawFloatRange(_attribute as TRangeFloat);
        }

        private void DrawFloatRange(TRangeFloat attribute)
        {
            DrawBase(attribute.Name, attribute.Step);
            GUILayout.Label($"=== Min:{attribute.Min} ===   === Max:{attribute.Max} ===");
            float value = Convert.ToSingle( _fieldInfo.GetValue(_instance));
            value = Mathf.Clamp(value, attribute.Min, attribute.Max);
            _fieldInfo.SetValue(_instance, value);
        }

        private void DrawFloat(TFloat attribute)
        {
            DrawBase(attribute.Name, attribute.Step);
        }

        private void DrawBase(string name, float[] step)
        {
            GUILayout.Label(name + " / Float" );
            _fieldInfo.SetValue(_instance, EditorGUILayout.FloatField("Slide by me",(float)_fieldInfo.GetValue(_instance)));
            GUILayout.BeginHorizontal();
            for (int i = step.Length-1; i >= 0; i--)
            {
                if(GUILayout.Button("-"+step[i]))
                    _fieldInfo.SetValue(_instance, (float)_fieldInfo.GetValue(_instance)-step[i]);
            }
            for (int i = 0; i < step.Length; i++)
            {
                if(GUILayout.Button("+"+step[i]))
                    _fieldInfo.SetValue(_instance, (float)_fieldInfo.GetValue(_instance)+step[i]);
            }
            GUILayout.EndHorizontal();
        }
    }
}