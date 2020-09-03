using System.Collections.Generic;

namespace Dotity
{
    public abstract class ExcuteSystem : IExcuteSystem
    {
        protected IGroup _group;
        public ExcuteSystem(IMatcher matcher)
        {
            _group = Group.CreateGroup(matcher);
        }
        public void Excute()
        {
            List<IEntity> entities = _group.GetEntities();
            for (int i = 0, length = entities.Count; i < length; i++)
            {
                if (ExcuteCondition(entities[i]))
                {
                    Excute(entities[i]);
                }
               
            }
        }
        public abstract void Excute(IEntity entity);
        public abstract bool ExcuteCondition(IEntity entity);
    }
}

