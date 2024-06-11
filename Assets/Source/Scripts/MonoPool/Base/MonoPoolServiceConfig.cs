using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MonoPool
{
    [CreateAssetMenu(fileName = nameof(MonoPoolServiceConfig), menuName = "Configs/MonoPool/Mono pool service config")]
    public class MonoPoolServiceConfig : SerializedScriptableObject
    {
        [field: SerializeField] public Vector3 SpawnPosition { get; private set; }
        
        [field: SerializeField] public Dictionary<Type, MonoPoolable> Poolables { get; private set; } = new();
        
#if UNITY_EDITOR
        public static IEnumerable<Type> PoolablesTypesGetter => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(MonoPoolable)));

        [Button]
        private void AddType([ValueDropdown(nameof(PoolablesTypesGetter))] Type type)
        {
            Poolables.Add(type, null);
        }
#endif
    }
}