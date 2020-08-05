
using Dotity;

public interface IMatcher
{
    ComponentKey[] GetAnyOf();
    ComponentKey[] GetNoneOf();
    bool Equal(IMatcher matcher);
    bool Match(IEntity enity);
}
