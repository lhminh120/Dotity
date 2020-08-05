using System.Collections.Generic;

namespace Dotity
{
    public class Entity : IEntity
    {
        #region Static Function
        private static Stack<IEntity> _enitiesReuse = new Stack<IEntity>();
        public static List<IEntity> _entities = new List<IEntity>();
        public static T Create<T>() where T : IEntity, new()
        {
            IEntity entity = _enitiesReuse.Count > 0 ? _enitiesReuse.Pop() : new T();
            _entities.Add(entity);
            return (T)entity;
        }
        public static void AddToReuseList(IEntity component)
        {
            _enitiesReuse.Push(component);
        }
        //Entity Is Not Really Destroyed, It's Just Added To Reuse List
        public static void DesTroyEntity(ref IEntity entity)
        {
            entity.RemoveAllComponents();
            IEntity temp = entity;
            AddToReuseList(temp);
            entity = null;
        }
        #endregion
        #region Function
        private Dictionary<short, IComponent> _components = new Dictionary<short, IComponent>();
        private bool _activeSelf = true;

        public delegate void OnEntityComponetChange();
        public event OnEntityComponetChange _onComponentAdded;
        public event OnEntityComponetChange _onComponentRemoved;

        public void SetActive(bool active) => _activeSelf = active;
        //Get Component By The Giving Key
        public IComponent GetComponent(ComponentKey componentKey)
        {
            short key = (short)componentKey;
            if (_components.ContainsKey(key))
                return _components[key];
            else
            {
                DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
                return null;
            }
        }

        //Add Component By The Giving Key
        public void AddComponent(ComponentKey componentKey, IComponent component)
        {
            short key = (short)componentKey;
            if (_components.ContainsKey(key))
            {
                DebugClass.Log("Already have this component", DebugKey.Dotity);
            }
            else
            {
                _components.Add(key, component);
            }
        }

        //Remove Component By The Giving Key
        //Component Not Really Being Removed
        //It's Just Pushed Into Stack Reuse And Save For Later
        public void RemoveComponent(ComponentKey componentKey)
        {
            short key = (short)componentKey;
            if (_components.ContainsKey(key))
            {
                Component.AddToReuseList(key, _components[key]);
                _components.Remove(key);
            }
            else
            {
                DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
            }
        }
        public void RemoveComponent(short componentKey)
        {
            if (_components.ContainsKey(componentKey))
            {
                Component.AddToReuseList(componentKey, _components[componentKey]);
                _components.Remove(componentKey);
            }
            else
            {
                DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
            }
        }

        public bool HasComponent(ComponentKey componentKey) => _components.ContainsKey((short)componentKey);
        public bool HasComponents(ComponentKey[] componentKeys)
        {
            for (int i = 0, length = componentKeys.Length; i < length; i++)
            {
                if (!HasComponent(componentKeys[i])) return false;
            }
            return true;
        }

        //Remove All Components
        public void RemoveAllComponents()
        {
            foreach (var item in _components.Keys)
            {
                RemoveComponent(item);
            }
        }

        #endregion
    }
}

