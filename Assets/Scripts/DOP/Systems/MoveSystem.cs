using Dotity;

public class MoveSystem : ExecuteSystem
{
    public MoveSystem() : base(new Matcher().AnyOf(ComponentKey.Position)) { }

    public override void Execute(IEntity entity)
    {
        Move(entity.GetComponent<PositionComponent>(ComponentKey.Position),
                entity.GetComponent<SpeedComponent>(ComponentKey.Speed));

    }

    public override bool ExecuteCondition(IEntity entity)
    {
        return true;
    }

    private void Move(PositionComponent position, SpeedComponent speed)
    {
        position.position.y += speed.speed * GameSystem._tick;// Time.deltaTime;
        if (position.position.y > 5)
        {
            position.position.y = -5;
        }
        position.HasChange();
    }
}
