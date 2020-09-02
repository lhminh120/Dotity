
using UnityEngine;

public class TransformComponent : Dotity.Component
{
    public Transform _transform;

    public TransformComponent(Transform transform)
    {
        _transform = transform;
    }

    public override ComponentKey GetComponentKey()
    {
        return ComponentKey.Transform;
    }
}
