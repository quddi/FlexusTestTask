using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace EntitiesPassing
{
    public abstract class MonoEntitySetter<T> : MonoBehaviour where T : class
    {
        [SerializeField, TabGroup("Parameters")] private string _key;
        
        [SerializeField, TabGroup("Components")] private T _entity;

        [Inject]
        private void Construct(IEntitiesPassingService entitiesPassingService)
        {
            entitiesPassingService.Set(_key, _entity);
        }
    }
}