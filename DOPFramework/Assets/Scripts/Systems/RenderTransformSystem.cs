
using Dotity;
using System.Collections.Generic;
using UnityEngine;

public class RenderTransformSystem : IExcuteSystem, ICleanUpSystem
{
    private IGroup _group;
    private Vector3 _vec = new Vector3();
    public RenderTransformSystem()
    {
        _group = Group.CreateGroup(Matcher.AnyOf(ComponentKey.Position, ComponentKey.Transform));
    }
    public void CleanUp()
    {
        List<IEntity> entities = _group.GetEntities();
        for (int i = 0, length = entities.Count; i < length; i++)
        {
            CleanUpRender(entities[i]);
        }
    }

    public void Excute()
    {
        List<IEntity> entities = _group.GetEntities();
        for (int i = 0, length = entities.Count; i < length; i++)
        {
            CheckNeedRender(entities[i]);
        }
    }
    private void CheckNeedRender(IEntity entity)
    {
        PositionComponent position = entity.GetComponent(ComponentKey.Position) as PositionComponent;
        if (position.IsChange())
        {
            TransformComponent transform = entity.GetComponent(ComponentKey.Transform) as TransformComponent;
            _vec.Set(position._position._x, position._position._y, position._position._z);
            transform._transform.position = _vec;
        }
    }
    private void CleanUpRender(IEntity entity)
    {
        PositionComponent position = entity.GetComponent(ComponentKey.Position) as PositionComponent;
        if (position.IsChange()) position.FinishChange();
    }
}
