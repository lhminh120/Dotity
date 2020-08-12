
using Dotity;
using System.Collections.Generic;
using UnityEngine;

public class RenderTransformSystem : RenderSystem
{
    private Vector3 _vec = new Vector3();
    public RenderTransformSystem() : base(Matcher.AnyOf(ComponentKey.Position, ComponentKey.Transform)) { }

    public override void Render(IEntity entity)
    {
        PositionComponent position = entity.GetComponent(ComponentKey.Position) as PositionComponent;
        if (!position.IsChange()) return;
        TransformComponent transform = entity.GetComponent(ComponentKey.Transform) as TransformComponent;
        CheckNeedRender(position, transform);
        CleanUpRender(position);
    }
    private void CheckNeedRender(PositionComponent position, TransformComponent transform)
    {
        _vec.Set(position._position._x, position._position._y, position._position._z);
        transform._transform.position = _vec;
    }
    private void CleanUpRender(PositionComponent position)
    {
        position.FinishChange();
    }
}
