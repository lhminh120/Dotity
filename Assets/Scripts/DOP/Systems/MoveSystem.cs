using System.Diagnostics;
using Dotity;

public class MoveSystem : ExecuteSystem
{
    public MoveSystem() : base(new Matcher().AnyOf(ComponentKey.Position)) { }

    public override void Execute(Entity entity)
    {
        Move(ref entity.GetComponent<PositionComponent>(ComponentKey.Position),
                ref entity.GetComponent<SpeedComponent>(ComponentKey.Speed));

    }

    public override bool ExecuteCondition(Entity entity)
    {
        return true;
    }

    private void Move(ref PositionComponent position, ref SpeedComponent speed)
    {
        position._position.y += speed._speed * GameSystem._tick;
        if (position._position.y > 5)
        {
            position._position.y = -5;
        }
        position.HasChanged();
    }
}
