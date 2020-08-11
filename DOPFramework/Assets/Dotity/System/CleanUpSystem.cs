using System.Collections.Generic;

namespace Dotity
{
    public abstract class CleanUpSystem : ICleanUpSystem
    {
        protected IGroup _group;
        public CleanUpSystem(IMatcher matcher)
        {
            _group = Group.CreateGroup(matcher);
        }
        public void CleanUp()
        {
            List<IEntity> entities = _group.GetEntities();
            for (int i = 0, length = entities.Count; i < length; i++)
            {
                CleanUp(entities[i]);
            }
        }
        public abstract void CleanUp(IEntity entity);
    }
}

