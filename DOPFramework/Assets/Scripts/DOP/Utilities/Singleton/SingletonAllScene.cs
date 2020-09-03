using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonAllScene<T> : MonoBehaviour where T : UnityEngine.Component
{

    #region Fields

    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }

        }
    }

    public virtual void OnApplicationQuit()
    {
        if (instance != null)
        {
            instance = null;
        }
    }


    public virtual void OnDestroy()
    {
        if (instance != null)
        {
            instance = null;
        }
    }

    #endregion

}
