
using System;
using System.Collections.Generic;
namespace Dotity
{
    public interface IGroup
    {
        IMatcher GetMatcher();
        bool Equal(IMatcher matcher);
        List<IEntity> GetEntities();
        bool Match(IEntity entity);
        bool Remove(IEntity entity);
        bool Add(IEntity entity);
        bool HasEntity(IEntity entity);
        void RegisterOnEntityAdded(Action<IEntity> onEntityAdded);
        void RegisterOnEntityRemoved(Action<IEntity> onEntityRemoved);
    }
}

