
using System.Collections.Generic;
using Dotity;

public class RenderTransformSystem_V2 : RenderSystem_V2
{
    private class RenderTransformSystemData
    {
        public int entityId;
        public PositionComponent position;
        public TransformComponent transform;
    }
    private List<RenderTransformSystemData> _data = new();
    public RenderTransformSystem_V2() : base(new Matcher().AnyOf(ComponentKey.Position, ComponentKey.Transform)) { }

    public override void Render()
    {
        for (int i = 0, length = _data.Count; i < length; i++)
        {
            if (_data[i].position.IsChange())
                RenderTransform(_data[i].position, _data[i].transform);
        }
    }

    private void RenderTransform(PositionComponent position, TransformComponent transform)
    {
        transform.transform.position = position.position;
        position.FinishChange();
    }

    public override void CreateRuntimeDataList()
    {
        var entities = _group.GetEntities();
        for (int i = 0, length = entities.Count; i < length; i++)
        {
            _data.Add(new RenderTransformSystemData()
            {
                entityId = entities[i].Id,
                position = entities[i].GetComponent<PositionComponent>(ComponentKey.Position),
                transform = entities[i].GetComponent<TransformComponent>(ComponentKey.Transform),
            });
        }
    }

    public override void OnEntityAdded(IEntity entity)
    {
        _data.Add(new RenderTransformSystemData()
        {
            entityId = entity.Id,
            position = entity.GetComponent<PositionComponent>(ComponentKey.Position),
            transform = entity.GetComponent<TransformComponent>(ComponentKey.Transform),
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
}
