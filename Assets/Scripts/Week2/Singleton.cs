using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            // get instance of singleton
            return instance;
        }
    }

    protected virtual void Awake()
    {
        // make it as dontdestroyobject        
        instance = this as T;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Say()
    {
        Debug.Log("Hello from Singleton!");
    }
}