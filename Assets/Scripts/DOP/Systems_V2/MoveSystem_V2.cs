using System.Collections.Generic;
using Dotity;

public class MoveSystem_V2 : ExecuteSystem_V2
{
    private class MoveSystemData
    {
        public int entityId;
        public PositionComponent position;
        public SpeedComponent speed;
    }
    private List<MoveSystemData> _data = new();
    public MoveSystem_V2() : base(new Matcher().AnyOf(ComponentKey.Position)) { }

    public override void CreateRuntimeDataList()
    {
        var entities = _group.GetEntities();
        for (int i = 0, length = entities.Count; i < length; i++)
        {
            _data.Add(new MoveSystemData()
            {
                entityId = entities[i].Id,
                position = entities[i].GetComponent<PositionComponent>(ComponentKey.Position),
                speed = entities[i].GetComponent<SpeedComponent>(ComponentKey.Speed),
            });
        }
    }

    public override void Execute()
    {
        for (int i = 0, length = _data.Count; i < length; i++)
        {
            Move(_data[i].position,
                   _data[i].speed);

        }
    }
    public override void OnEntityAdded(IEntity entity)
    {
        _data.Add(new MoveSystemData()
        {
            entityId = entity.Id,
            position = entity.GetComponent<PositionComponent>(ComponentKey.Position),
            speed = entity.GetComponent<SpeedComponent>(ComponentKey.Speed),
        });
    }

    public override void OnEntityRemoved(IEntity entity)
    {
        for (int i = 0, length = _data.Count; i < length; i++)
        {
            if (_data[i].entityId == entity.Id)
            {
                _data.RemoveAt(i);
                break;
            }
        }
    }

    private void Move(PositionComponent position, SpeedComponent speed)
    {
        position.position.y += speed.speed * GameSystem._tick;
        if (position.position.y > 5)
        {
            position.position.y = -5;
        }
        position.HasChange();
    }
}
