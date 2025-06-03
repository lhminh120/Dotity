using System.Collections.Generic;

namespace Dotity
{
    public class Group : IGroup
    {
        #region Static Function
        private static readonly List<IGroup> _groups = new();
        public static IGroup CreateGroup(IMatcher matcher)
        {
            for (int i = 0, length = _groups.Count; i < length; i++)
            {
                if (_groups[i].Equal(matcher)) return _groups[i];
            }
            IGroup group = new Group(matcher);
            _groups.Add(group);
            return group;
        }
        public static void OnEntityRemoveComponent(IEntity entity)
        {
            for (int i = _groups.Count - 1; i >= 0; i--)
            {
                if (!_groups[i].Match(entity))
                {
                    _groups[i].Remove(entity);
                }
            }
        }
        public static void OnEntityAddComponent(IEntity entity)
        {
            for (int i = 0, length = _groups.Count; i < length; i++)
            {
                if (_groups[i].Match(entity))
                {
                    _groups[i].Add(entity);
                }
            }
        }
        #endregion
        #region Function
        private readonly IMatcher _matcher;
        private readonly List<IEntity> _entities = new();
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
        public bool Match(IEntity entity) => _matcher.Match(entity);
        public bool Remove(IEntity entity) => _entities.Remove(entity);
        public bool Add(IEntity entity)
        {
            if (!_entities.Contains(entity))
            {
                _entities.Add(entity);
                return true;
            }
            return false;
        }
        public bool HasEntity(IEntity entity) => _entities.Contains(entity);
        #endregion
    }
}

