
using System;

namespace Dotity
{
    public interface IMatcher
    {
        Type[] GetAnyOf();
        Type[] GetNoneOf();
        bool Equal(IMatcher matcher);
        bool Match(Entity entity);
    }
}

