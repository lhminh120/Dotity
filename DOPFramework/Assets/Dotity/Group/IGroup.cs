
using Dotity;
using System.Collections.Generic;

public interface IGroup
{
    IMatcher GetMatcher();
    bool Equal(IMatcher matcher);
    List<IEntity> GetEntities();
    bool Match(IEntity entity);
    bool Remove(IEntity entity);
    bool Add(IEntity entity);
    bool HasEntity(IEntity entity);
}
