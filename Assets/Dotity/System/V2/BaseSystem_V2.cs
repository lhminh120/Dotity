using System.Collections.Generic;

namespace Dotity
{
    public abstract class BaseSystem_V2
    {
        protected IGroup _group;
        public BaseSystem_V2(IMatcher matcher)
        {
            _group = Group.CreateGroup(matcher);
            _group.RegisterOnEntityAdded(OnEntityAdded);
            _group.RegisterOnEntityRemoved(OnEntityRemoved);
            CreateRuntimeDataList();
        }
        public abstract void CreateRuntimeDataList();
        public abstract void OnEntityAdded(IEntity entity);
        public abstract void OnEntityRemoved(IEntity entity);
    }
}

