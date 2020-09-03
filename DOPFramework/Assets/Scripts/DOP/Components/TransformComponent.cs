
using UnityEngine;

public class TransformComponent : Dotity.Component
{
    public Transform _transform;
    private ComponentKey _key = ComponentKey.Transform;

    public TransformComponent(Transform transform)
    {
        _transform = transform;
    }
    public override ComponentKey Key => _key;
}
