
using UnityEngine;

public class GameObjectComponent : Dotity.Component
{
    public GameObject _obj;

    public GameObjectComponent(GameObject obj)
    {
        _obj = obj;
    }

    public override ComponentKey GetComponentKey()
    {
        return ComponentKey.GameObject;
    }
}
