using UnityEngine;

namespace Game
{
    public class SceneScope : CustomScope
    {
        private void OnValidate()
        {
            if (autoRun == false)
                Debug.LogWarning("The scene scope must run automatically!");
            
            autoRun = true;
        }
    }
}