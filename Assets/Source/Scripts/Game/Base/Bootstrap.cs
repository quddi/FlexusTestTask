using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Bootstrap : SerializedMonoBehaviour
    {
#if UNITY_EDITOR
        [field: ValueDropdown("@EditorGet.BuiltScenesNames")]
#endif
        [SerializeField] private string _singleSceneName;
        
#if UNITY_EDITOR
        [field: ValueDropdown("@EditorGet.BuiltScenesNames")]
#endif
        [SerializeField] private List<string> _additiveScenesNames = new();

        private void Start()
        {
            SceneManager.LoadScene(_singleSceneName, LoadSceneMode.Single);
            
            foreach (var sceneName in _additiveScenesNames)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }
}