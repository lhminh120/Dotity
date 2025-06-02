using System.Collections.Generic;

namespace Dotity
{
    public class Group : IGroup
    {
        #region Static Function
        private static readonly List<IGroup> _groups = new List<IGroup>();
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
        public static void OnEntityRemoveComponent(Entity entity)
        {
            for (int i = _groups.Count - 1; i >= 0; i--)
            {
                if (!_groups[i].Match(entity))
                {
                    _groups[i].Remove(entity);
                }
            }
        }
        public static void OnEntityAddComponent(Entity entity)
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
        private readonly List<Entity> _entities = new List<Entity>();
        public Group(IMatcher matcher)
        {
            _matcher = matcher;
            for (int i = 0, length = EntityPool.entities.Count; i < length; i++)
            {
                if (_matcher.Match(EntityPool.entities[i]))
                {
                    _entities.Add(EntityPool.entities[i]);
                }
            }
        }
        public bool Equal(IMatcher matcher) => _matcher.Equal(matcher);
        public IMatcher GetMatcher() => _matcher;
        public List<Entity> GetEntities() => _entities;
        public bool Match(Entity entity) => _matcher.Match(entity);

        public bool Remove(Entity entity) => _entities.Remove(entity);

        public bool Add(Entity entity)
        {
            if (!_entities.Contains(entity))
            {
                _entities.Add(entity);
                return true;
            }
            return false;
        }

        public bool HasEntity(Entity entity) => _entities.Contains(entity);
        #endregion
    }
}

