using System.Collections.Generic;

namespace Dotity
{
    public abstract class InitializeSystem : BaseSystem, IInitializeSystem
    {
        public InitializeSystem(IMatcher matcher) : base(matcher) { }
        public void Initialize()
        {
            List<IEntity> entities = _group.GetEntities();
            for (int i = 0, length = entities.Count; i < length; i++)
            {
                Initialize(entities[i]);
            }
        }
        public abstract void Initialize(IEntity entity);
    }
}

