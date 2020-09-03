
using Dotity;

public class RenderTransformSystem : RenderSystem
{
    public RenderTransformSystem() : base(new Matcher().AnyOf(ComponentKey.Position, ComponentKey.Transform)) { }

    public override void Render(IEntity entity)
    {
        PositionComponent position = entity.GetComponent<PositionComponent>(ComponentKey.Position);
        TransformComponent transform = entity.GetComponent<TransformComponent>(ComponentKey.Transform);
        RenderTransform(position, transform);
    }

    public override bool RenderCondition(IEntity entity)
    {
        return entity.GetComponent<PositionComponent>(ComponentKey.Position).IsChange();
    }

    private void RenderTransform(PositionComponent position, TransformComponent transform)
    {
        //_vec.Set(position._position._x, position._position._y, position._position._z);
        transform._transform.position = position._position;
        position.FinishChange();
    }
}
