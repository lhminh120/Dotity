
using System.Collections.Generic;
namespace Dotity
{
    public interface IGroup
    {
        IMatcher GetMatcher();
        bool Equal(IMatcher matcher);
        List<Entity> GetEntities();
        bool Match(Entity entity);
        bool Remove(Entity entity);
        bool Add(Entity entity);
        bool HasEntity(Entity entity);
    }
}

