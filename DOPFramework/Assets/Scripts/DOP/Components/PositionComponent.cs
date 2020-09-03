
using UnityEngine;

public class PositionComponent : Dotity.Component
{
    public Vector3 _position;
    private ComponentKey _key = ComponentKey.Position;
    public PositionComponent(Vector3 position)
    {
        _position = position;
    }
    public override ComponentKey Key => _key;
}
