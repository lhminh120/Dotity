
using Dotity;

public class RenderTransformSystem : RenderSystem
{
    public RenderTransformSystem() : base(new Matcher().AnyOf(typeof(PositionComponent), typeof(TransformComponent))) { }

    public override void Render(Entity entity)
    {
        RenderTransform(ref entity.GetComponent<PositionComponent>(),
                    ref entity.GetComponent<TransformComponent>());
    }

    public override bool RenderCondition(Entity entity)
    {
        return entity.GetComponent<PositionComponent>().IsChanged();
    }

    private void RenderTransform(ref PositionComponent position, ref TransformComponent transform)
    {
        // UnityEngine.Debug.Log("position (2) " + position._position);
        //_vec.Set(position._position._x, position._position._y, position._position._z);
        transform._transform.position = position._position;
        position.FinishChanged();
    }
}
