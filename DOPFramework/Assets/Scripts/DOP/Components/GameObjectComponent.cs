
using UnityEngine;

public class GameObjectComponent : Dotity.Component
{
    public GameObject _obj;
    private ComponentKey _key = ComponentKey.GameObject;

    public GameObjectComponent(GameObject obj)
    {
        _obj = obj;
    }

    public override ComponentKey Key => _key;

}
