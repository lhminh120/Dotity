using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dotity
{
    public class Group : IGroup
    {
        #region Static Function
        private static List<IGroup> _groups = new List<IGroup>();
        public IGroup GetGroup(IMatcher matcher)
        {
            for (int i = 0, length = _groups.Count; i < length; i++)
            {
                if (_groups[i].Equals(matcher)) return _groups[i];
            }
            IGroup group = new Group(matcher);
            _groups.Add(group);
            return group;
        }
        public void OnEntityRemoveComponent()
        {

        }
        public void OnEntityAddComponent()
        {

        }
        #endregion
        #region Function
        private IMatcher _matcher;
        private List<IEntity> _entities = new List<IEntity>();
        public Group(IMatcher matcher)
        {
            _matcher = matcher;
            for (int i = 0, length = Entity._entities.Count; i < length; i++)
            {
                if (_matcher.Match(Entity._entities[i]))
                {
                    _entities.Add(Entity._entities[i]);
                }
            }
        }
        public bool Equal(IMatcher matcher) => _matcher.Equal(matcher);
        public IMatcher GetMatcher() => _matcher;
        public List<IEntity> GetEntities() => _entities;
        #endregion
    }
}

