
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace DI
{
    public class InjectAttribute : System.Attribute
    {
        public string key;

        public InjectAttribute()
        {

        }

        public InjectAttribute(string key)
        {
            this.key = key;
        }
    }


    public class InjectObj
    {
        bool isInjected = false;

        public void CheckAndInject(object taget)
        {
            if (isInjected)
                return;
            isInjected = true;
            DIContainer.Inject(taget);
        }
    }


    public class DIContainer
    {
        static DIContainer _global = new DIContainer();
        public static DIContainer Global { get { return _global; } }

        public static DIContainer Local { get; set; }

        public static void Inject(object injectionTargetObject)
        {
            var fieldInfos = injectionTargetObject.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var fi in fieldInfos)
            {
                var injectAttr = fi.GetCustomAttribute<InjectAttribute>();
                if (injectAttr == null)
                {
                    continue;
                }

                object value=null;
                if (Local != null)
                {
                    value = Local.GetValue(fi.FieldType, injectAttr.key);
                }

                if (value == null)
                {
                    value = Global.GetValue(fi.FieldType, injectAttr.key);
                }
                if (value == null)
                {
                    ThrowNotContainException(fi, injectAttr);
                }

                fi.SetValue(injectionTargetObject, value);
            }

        }

        public object GetValue(Type type,string key = null)
        {
            var strKey= GetKey(type, key);
            if (containerDic.ContainsKey(strKey) == false)
                return null;
            return containerDic[strKey];
        }


        Dictionary<string, object> containerDic = new Dictionary<string, object>();
        
     
   
      
        private static void ThrowNotContainException(FieldInfo fi, InjectAttribute injectAttr)
        {
            if (string.IsNullOrEmpty(injectAttr.key))
            {
                throw new Exception($"{fi.FieldType}가 등록되지 않았습니다.");
            }
            else
            {
                throw new Exception($"{fi.FieldType}에 {injectAttr.key}가 등록되지 않았습니다.");
            }
        }


        public string GetKey(Type type, string key=null)
        {
            if (key != null)
            {
                return type.Name + ":" + key;
            }
            return type.Name;
        }

        public void Regist<T>(T t){
            containerDic.Add(GetKey(typeof(T)),t);
        }
        public void Regist<T>(T t, string key)
        {
            containerDic.Add(GetKey(typeof(T),key), t);
        }
    }
}
