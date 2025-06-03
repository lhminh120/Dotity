
using UnityEngine;

public class PositionComponent : Dotity.Component
{
    public Vector3 position;
    private ComponentKey _key = ComponentKey.Position;
    public PositionComponent() { }
    public PositionComponent(Vector3 position)
    {
        this.position = position;
    }
    public PositionComponent Init(Vector3 position)
    {
        this.position = position;
        return this;
    }
    public override ComponentKey Key => _key;
}
