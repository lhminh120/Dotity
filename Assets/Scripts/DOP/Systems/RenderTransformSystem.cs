
using Dotity;

public class RenderTransformSystem : RenderSystem
{
    public RenderTransformSystem() : base(new Matcher().AnyOf(ComponentKey.Position, ComponentKey.Transform)) { }

    public override void Render(Entity entity)
    {
        RenderTransform(ref entity.GetComponent<PositionComponent>(ComponentKey.Position),
                    ref entity.GetComponent<TransformComponent>(ComponentKey.Transform));
    }

    public override bool RenderCondition(Entity entity)
    {
        return entity.GetComponent<PositionComponent>(ComponentKey.Position).IsChanged();
    }

    private void RenderTransform(ref PositionComponent position, ref TransformComponent transform)
    {
        transform._transform.position = position._position;
        position.FinishChanged();
    }
}
