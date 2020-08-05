
using Dotity;
using System.Collections.Generic;

public interface IGroup
{
    IMatcher GetMatcher();
    bool Equal(IMatcher matcher);
    List<IEntity> GetEntities();
}
