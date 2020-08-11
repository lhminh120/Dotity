
using UnityEngine;
using UnityEngine.UIElements;

public class GameData : SingletonOneScene<GameData>
{
    public GameObject _object;
    public float _speed;
    public GameObject CreateObject()
    {
        Vector3 pos = new Vector3(Random.Range(-10f, 10f), Random.Range(-3f, 3f), 0);
        return Instantiate(_object, pos, Quaternion.identity);
    }
}
