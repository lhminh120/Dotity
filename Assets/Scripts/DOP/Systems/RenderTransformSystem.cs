
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
        transform.transform.position = position.position;
        position.FinishChange();
    }
}
