
using System.Collections.Generic;

namespace Dotity
{
    public abstract class RenderSystem : BaseSystem, IRenderSystem
    {
        public RenderSystem(IMatcher matcher) : base(matcher) { }
        public void Render()
        {
            List<IEntity> entities = _group.GetEntities();
            for (int i = 0, length = entities.Count; i < length; i++)
            {
                if (RenderCondition(entities[i]))
                {
                    Render(entities[i]);
                }

            }
        }
        public abstract void Render(IEntity entity);
        public abstract bool RenderCondition(IEntity entity);
    }
}

