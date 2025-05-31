
using System.Collections.Generic;

namespace Dotity
{
    public abstract class RenderSystem : IRenderSystem
    {
        protected IGroup _group;
        public RenderSystem(IMatcher matcher)
        {
            _group = Group.CreateGroup(matcher);
        }
        public void Render()
        {
            List<Entity> entities = _group.GetEntities();
            for (int i = 0, length = entities.Count; i < length; i++)
            {
                if (RenderCondition(entities[i]))
                {
                    Render(entities[i]);
                }

            }
        }
        public abstract void Render(Entity entity);
        public abstract bool RenderCondition(Entity entity);
    }
}

