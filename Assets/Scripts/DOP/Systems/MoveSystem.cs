using System.Diagnostics;
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
        position._position.y += speed._speed * GameSystem._tick;// Time.deltaTime;
        if (position._position.y > 5)
        {
            position._position.y = -5;
        }
        // UnityEngine.Debug.Log("position (1) " + position._position);
        position.HasChanged();
    }
}
