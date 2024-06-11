using System.Collections.Generic;
using System.Linq;
using UnityEditor;

#if UNITY_EDITOR

namespace Extensions
{
    public static class EditorGet
    {
        public static IEnumerable<string> BuiltScenesNames
        {
            get
            {
                var guids = AssetDatabase.FindAssets("t:Scene");

                return guids.Select(SceneNameSelector);

                string SceneNameSelector(string guid)
                {
                    var fullPath = AssetDatabase.GUIDToAssetPath(guid);
                    return AssetDatabase.LoadAssetAtPath(fullPath, typeof(SceneAsset)).name;
                }
            }
        }
    }
}
#endif
