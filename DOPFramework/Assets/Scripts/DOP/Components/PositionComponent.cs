
using Dotity;

public class PositionComponent : Component
{
    public Point3D _position;
    private ComponentKey _key = ComponentKey.Position;
    public PositionComponent(Point3D position)
    {
        _position = position;
    }
    public override ComponentKey Key => _key;
}
