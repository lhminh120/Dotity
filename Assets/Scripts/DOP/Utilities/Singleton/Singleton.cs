using UnityEngine;

public interface Singleton<T> where T : class
{
    public static T Instance;
}