using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameManager : MonoBehaviour
{
    public GameObject _obj;
    public int _count = 10000;
    private void Awake()
    {
        for (int i = 0; i < _count; i++)
        {
            Vector3 vec = new Vector3(Random.Range(-10f, 10f), Random.Range(-3f, 3f), 0);
            Instantiate(_obj, vec, Quaternion.identity);
        }
    }
}
