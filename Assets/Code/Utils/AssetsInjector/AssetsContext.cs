using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Aivagames.Strategy.Utils.AssetsInjector
{
    [CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "Strategy/" + nameof(AssetsContext), order = 0)]
    public class AssetsContext : ScriptableObject
    {
        [SerializeField] private Object[] _objects;

        public Object GetObjectType(Type targetType, string targetName = null)
        {
            foreach (var obj in _objects)
            {
                if (obj.GetType().IsAssignableFrom(targetType))
                {
                    if (targetName == null || obj.name == targetName)
                    {
                        return obj;
                    }
                }
            }

            return null;
        }
    }
}