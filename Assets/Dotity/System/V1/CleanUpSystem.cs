using System.Collections.Generic;

namespace Dotity
{
    public abstract class CleanUpSystem : BaseSystem, ICleanUpSystem
    {
        public CleanUpSystem(IMatcher matcher) : base(matcher) { }
        public void CleanUp()
        {
            List<IEntity> entities = _group.GetEntities();
            for (int i = 0, length = entities.Count; i < length; i++)
            {
                if (CleanUpCondition(entities[i]))
                {
                    CleanUp(entities[i]);
                }

            }
        }
        public abstract void CleanUp(IEntity entity);
        public abstract bool CleanUpCondition(IEntity entity);
    }
}

