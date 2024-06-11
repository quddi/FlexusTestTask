using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = nameof(UiServiceConfig), menuName = "Configs/Ui/Ui service config")]
    public class UiServiceConfig : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<Type, Window> WindowsPrefabs { get; private set; } = new();
        
#if UNITY_EDITOR
        public static IEnumerable<Type> WindowsTypesGetter => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(Window)));

        [Button]
        private void AddType([ValueDropdown(nameof(WindowsTypesGetter))] Type type)
        {
            WindowsPrefabs.Add(type, null);
        }
#endif
    }
}