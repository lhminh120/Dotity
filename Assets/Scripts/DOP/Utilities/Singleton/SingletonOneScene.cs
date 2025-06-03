using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonOneScene<T> : MonoBehaviour where T : UnityEngine.Component
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
            }
            return instance;
        }
    }

    #endregion

    #region Methods



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
