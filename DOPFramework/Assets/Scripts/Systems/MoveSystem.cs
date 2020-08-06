using Dotity;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : IExcuteSystem
{
    private IGroup _group;
    public MoveSystem()
    {
        _group = Group.CreateGroup(Matcher.AnyOf(ComponentKey.Position));
    }
    public void Excute()
    {
        List<IEntity> entities = _group.GetEntities();
        for (int i = 0, length = entities.Count; i < length; i++)
        {
            Move(entities[i].GetComponent(ComponentKey.Position) as PositionComponent,
                entities[i].GetComponent(ComponentKey.Speed) as SpeedComponent);
        }
    }
    private void Move(PositionComponent position, SpeedComponent speed)
    {
        position._position._y += speed._speed * Time.deltaTime;
        if(position._position._y > 5)
        {
            position._position._y = -5;
        }
        position.HasChange();
    }
}
