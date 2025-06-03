
using UnityEngine;

public class TransformComponent : Dotity.Component
{
    public Transform transform;
    private ComponentKey _key = ComponentKey.Transform;
    public TransformComponent() { }
    public TransformComponent(Transform transform)
    {
        this.transform = transform;
    }
    public TransformComponent Init(Transform transform)
    {
        this.transform = transform;
        return this;
    }
    public override ComponentKey Key => _key;
}
