
using Dotity;

public class PositionComponent : Component
{
    public Point3D _position;

    public PositionComponent(Point3D position)
    {
        _position = position;
    }

    public override ComponentKey GetComponentKey()
    {
        return ComponentKey.Position;
    }
}
