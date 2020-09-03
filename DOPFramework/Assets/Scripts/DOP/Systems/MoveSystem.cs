using Dotity;

//public class MoveSystem : JobSystem
//{
//    public MoveSystem() : base(8, Matcher.AnyOf(ComponentKey.Position)) { }

//    public override void JobExcute(IEntity entity)
//    {
//        Move(entity.GetComponent<PositionComponent>(ComponentKey.Position),
//                entity.GetComponent<SpeedComponent>(ComponentKey.Speed));
//    }

//    private void Move(PositionComponent position, SpeedComponent speed)
//    {
//        position._position._y += speed._speed * GameSystem._tick;// Time.deltaTime;
//        if(position._position._y > 5)
//        {
//            position._position._y = -5;
//        }
//        position.HasChange();
//    }
//}

public class MoveSystem : ExcuteSystem
{
    public MoveSystem() : base(new Matcher().AnyOf(ComponentKey.Position)) { }

    public override void Excute(IEntity entity)
    {
        Move(entity.GetComponent<PositionComponent>(ComponentKey.Position),
                entity.GetComponent<SpeedComponent>(ComponentKey.Speed));

    }

    public override bool ExcuteCondition(IEntity entity)
    {
        return true;
    }

    private void Move(PositionComponent position, SpeedComponent speed)
    {
        position._position.y += speed._speed * GameSystem._tick;// Time.deltaTime;
        if (position._position.y > 5)
        {
            position._position.y = -5;
        }
        position.HasChange();
    }
}
