
using Dotity;

public class SpeedComponent : Component
{
    public float _speed;
    private ComponentKey _key = ComponentKey.Speed;
    public SpeedComponent(float speed)
    {
        _speed = speed;
    }
    public override ComponentKey Key => _key;
}
