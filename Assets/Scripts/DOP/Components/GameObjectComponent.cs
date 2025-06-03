
using UnityEngine;

public class GameObjectComponent : Dotity.Component
{
    public GameObject obj;
    private ComponentKey _key = ComponentKey.GameObject;

    public GameObjectComponent() { }
    public GameObjectComponent(GameObject obj)
    {
        this.obj = obj;
    }
    public GameObjectComponent Init(GameObject obj)
    {
        this.obj = obj;
        return this;
    }

    public override ComponentKey Key => _key;

}
