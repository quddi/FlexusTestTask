using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    public static class ExtensionMethods
    {
        public static Vector3 GetRandomPoint(this Bounds bounds) 
        {
            var minX = bounds.size.x * 0.5f;
            var minY = bounds.size.y * 0.5f;
            var minZ = bounds.size.z * 0.5f;

            return new Vector3(
                UnityEngine.Random.Range(minX, -minX), 
                UnityEngine.Random.Range(minY, -minY), 
                UnityEngine.Random.Range(minZ, -minZ));
        }
        
        public static Vector3 GetRandomPointInBounds(this BoxCollider boundsCollider)
        {
            var position = boundsCollider.transform.position;

            return position + boundsCollider.bounds.GetRandomPoint();
        }

        public static float RandomFromInterval(this (float Min, float Max) interval)
        {
            return UnityEngine.Random.Range(interval.Min, interval.Max);
        }
        
        public static int RandomFromInterval(this (int Min, int Max) interval)
        {
            return UnityEngine.Random.Range(interval.Min, interval.Max + 1);
        }
        
        public static T SnatchFirst<T>(this HashSet<T> hashSet)
        {
            var element = hashSet.First();

            hashSet.Remove(element);

            return element;
        }

        public static T2 Snatch<T1, T2>(this Dictionary<T1, T2> dictionary, T1 key)
        {
            var value = dictionary[key];

            dictionary.Remove(key);
            
            return value;
        }
        
        public static T SnatchRandom<T>(this IList<T> list)
        {
            if (list.Count == 0) return default;
            
            var randomIndex = UnityEngine.Random.Range(0, list.Count);

            var selectedElement = list[randomIndex];
                
            list.RemoveAt(randomIndex);

            return selectedElement;
        }
        
        public static T Random<T>(this IList<T> list)
        {
            if (list.Count == 0)
                throw new IndexOutOfRangeException("List needs at least one entry to call Random()");

            if (list.Count == 1)
                return list[0];

            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static void RemoveRange<T>(this HashSet<T> source, HashSet<T> removed)
        {
            foreach (var removedElement in removed)
            {
                source.Remove(removedElement);
            }
        }
        
        public static Vector3 ShiftRandomly(this Vector3 vector3, float delta)
        {
            return new Vector3
            {
                x = vector3.x + UnityEngine.Random.Range(-delta, delta),
                y = vector3.y + UnityEngine.Random.Range(-delta, delta),
                z = vector3.z + UnityEngine.Random.Range(-delta, delta),
            };
        }

        public static Vector3 WithX(this Vector3 vector3, float newX)
        {
            return new Vector3 { x = newX, y = vector3.y, z = vector3.z };
        }
        
        public static Vector3 WithY(this Vector3 vector3, float newY)
        {
            return new Vector3 { x = vector3.x, y = newY, z = vector3.z };
        }
        
        public static Vector3 WithZ(this Vector3 vector3, float newZ)
        {
            return new Vector3 { x = vector3.z, y = vector3.y, z = newZ };
        }
        
        public static bool IsInLMask(this int layer, LayerMask mask)
        {
            return ((mask.value & (1 << layer)) != 0);
        }

        public static void LootInDirection(this Transform transform, Vector3 direction)
        {
            var point = transform.position + direction;
            
            transform.LookAt(point);
        }
        
#if UNITY_EDITOR
        public static IEnumerable<T> GetAllScriptableObjects<T>() where T : ScriptableObject
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).Name);
            
            foreach (var guid in guids)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
                
                yield return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            }
        }
#endif
    }
}
