using System.Collections.Generic;

namespace Dotity
{
    public abstract class InitializeSystem : IInitializeSystem
    {
        protected IGroup _group;
        public InitializeSystem(IMatcher matcher)
        {
            _group = Group.CreateGroup(matcher);
        }
        public void Initialize()
        {
            List<Entity> entities = _group.GetEntities();
            for (int i = 0, length = entities.Count; i < length; i++)
            {
                Initialize(entities[i]);
            }
        }
        public abstract void Initialize(Entity entity);
    }
}

