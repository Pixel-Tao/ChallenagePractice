using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
            }
            
            // get instance of singleton
            return instance;
        }
    }

    protected virtual void Awake()
    {
        instance = this as T;
    }

    public void Say()
    {
        Debug.Log("Hello from Singleton!");
    }
}