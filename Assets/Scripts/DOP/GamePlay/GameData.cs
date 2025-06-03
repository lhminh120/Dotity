
using UnityEngine;
using UnityEngine.UIElements;

public class GameData : MonoBehaviour
{
    public GameObject _object;
    public float _speed;
    private void Awake()
    {
        Singleton<GameData>.Instance = this;
    }
    public GameObject CreateObject()
    {
        Vector3 pos = new Vector3(Random.Range(-10f, 10f), Random.Range(-3f, 3f), 0);

        return Instantiate(_object, pos, Quaternion.identity);
    }
}
