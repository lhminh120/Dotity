using System;
using System.Collections.Generic;

namespace Dotity
{
    public class Entity : IEntity
    {
        #region Static Function
        private static Stack<IEntity> _enitiesReuse = new Stack<IEntity>();
        public static List<IEntity> _entities = new List<IEntity>();
        public static Entity CreateEntity()
        {
            Entity entity = _enitiesReuse.Count > 0 ? (Entity)_enitiesReuse.Pop() : new Entity();
            entity.RegisteCallBackAddedComponent(Group.OnEntityAddComponent);
            entity.RegisteCallBackRemovedComponent(Group.OnEntityRemoveComponent);
            _entities.Add(entity);
            return entity;
        }
        public static void AddToReuseList(IEntity entity)
        {
            _enitiesReuse.Push(entity);
        }
        //Entity Is Not Really Destroyed, It's Just Added To Reuse List
        public static void DesTroyEntity(ref IEntity entity)
        {
            entity.RemoveAllComponents();
            entity.RemoveAllCallBack();
            IEntity temp = entity;
            AddToReuseList(temp);
            entity = null;
        }
        #endregion
        #region Function
        private Dictionary<int, IComponent> _components = new Dictionary<int, IComponent>();
        private bool _activeSelf = true;

        
        private Action<IEntity> _onComponentAdded;
        private Action<IEntity> _onComponentRemoved;
        
        public void SetActive(bool active) => _activeSelf = active;
        //Get Component By The Giving Key
        public IComponent GetComponent(ComponentKey componentKey)
        {
            int key = (int)componentKey;
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
            int key = (int)componentKey;
            if (_components.ContainsKey(key))
            {
                DebugClass.Log("Already have this component", DebugKey.Dotity);
            }
            else
            {
                _components.Add(key, component);
                _onComponentAdded?.Invoke(this);
            }
        }

        //Remove Component By The Giving Key
        //Component Not Really Being Removed
        //It's Just Pushed Into Stack Reuse And Save For Later
        public void RemoveComponent(ComponentKey componentKey)
        {
            int key = (int)componentKey;
            if (_components.ContainsKey(key))
            {
                Component.AddToReuseList(key, _components[key]);
                _components.Remove(key);
                _onComponentRemoved?.Invoke(this);
            }
            else
            {
                DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
            }
        }
        private void RemoveComponent(int componentKey)
        {
            if (_components.ContainsKey(componentKey))
            {
                Component.AddToReuseList(componentKey, _components[componentKey]);
                _components.Remove(componentKey);
                _onComponentRemoved?.Invoke(this);
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

        public void RegisteCallBackAddedComponent(Action<IEntity> onEntityComponetAdded)
        {
            _onComponentAdded = onEntityComponetAdded;
        }

        public void RegisteCallBackRemovedComponent(Action<IEntity> onEntityComponetRemoved)
        {
            _onComponentRemoved = onEntityComponetRemoved;
        }

        public void RemoveAllCallBack()
        {
            _onComponentAdded = null;
            _onComponentRemoved = null;
        }
        #endregion
    }
}

