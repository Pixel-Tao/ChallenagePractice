using UnityEngine;

public static class ExtensionMethods
{
    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        if (!obj.TryGetComponent<T>(out T component))
            component = obj.gameObject.AddComponent<T>();

        return component;
    }
}
