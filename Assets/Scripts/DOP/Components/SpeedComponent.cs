
using Dotity;

public class SpeedComponent : Component
{
    public float speed;
    private ComponentKey _key = ComponentKey.Speed;
    public SpeedComponent() { }
    public SpeedComponent(float speed)
    {
        this.speed = speed;
    }
    public SpeedComponent Init(float speed)
    {
        this.speed = speed;
        return this;
    }
    public override ComponentKey Key => _key;
}
