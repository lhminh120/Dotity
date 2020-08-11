using Dotity;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : JobSystem
{
    public MoveSystem() : base(8, Matcher.AnyOf(ComponentKey.Position)) { }

    public override void JobExcute(IEntity entity)
    {
        Move(entity.GetComponent(ComponentKey.Position) as PositionComponent,
                entity.GetComponent(ComponentKey.Speed) as SpeedComponent);
    }

    private void Move(PositionComponent position, SpeedComponent speed)
    {
        position._position._y += speed._speed * GameManager._tick;// Time.deltaTime;
        if(position._position._y > 5)
        {
            position._position._y = -5;
        }
        position.HasChange();
    }
}
