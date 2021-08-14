using System;
using System.Reflection;
using Plugins.HabObject.Customizable.Attributs;
using UnityEngine;

namespace Plugins.HabObject.Customizable.ViewAttribute
{
    public abstract class ViewFieldCustomizableComponent
    {
        public abstract void OnGUI();

        public static ViewFieldCustomizableComponent CreateView(TField attribute, FieldInfo fieldInfo, MonoBehaviour instance)
        {
            if (attribute is TFloat || attribute is TRangeFloat)
                return new ViewTFloat(attribute, fieldInfo, instance);
            if (attribute is TInt || attribute is TRangeInt)
                return new ViewInt(attribute, fieldInfo, instance);
            if (attribute is TString)
                return new ViewString(attribute as TString, fieldInfo, instance);
            if (attribute is TEnum)
                return new ViewEnum(attribute as TEnum, fieldInfo, instance);
            if (attribute is TObject)
                return new ViewObject(attribute as TObject, fieldInfo, instance);
            
            throw new Exception("Unknow attribute");
        }
    }
}