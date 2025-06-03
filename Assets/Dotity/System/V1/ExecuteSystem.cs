using System.Collections.Generic;

namespace Dotity
{
    public abstract class ExecuteSystem : BaseSystem, IExecuteSystem
    {
        public ExecuteSystem(IMatcher matcher) : base(matcher) { }
        public void Execute()
        {
            List<IEntity> entities = _group.GetEntities();
            for (int i = 0, length = entities.Count; i < length; i++)
            {
                if (ExecuteCondition(entities[i]))
                {
                    Execute(entities[i]);
                }

            }
        }
        public abstract void Execute(IEntity entity);
        public abstract bool ExecuteCondition(IEntity entity);
    }
}

