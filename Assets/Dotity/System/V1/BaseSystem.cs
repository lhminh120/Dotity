
namespace Dotity
{
    public abstract class BaseSystem
    {
        protected IGroup _group;
        public BaseSystem(IMatcher matcher)
        {
            _group = Group.CreateGroup(matcher);
        }
    }
}

