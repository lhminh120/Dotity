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
        //private Dictionary<int, IComponent> _components = new Dictionary<int, IComponent>();
        private List<IComponent> _components = new List<IComponent>();
        private bool _activeSelf = true;


        private Action<IEntity> _onComponentAdded;
        private Action<IEntity> _onComponentRemoved;

        public void SetActive(bool active) => _activeSelf = active;
        //Get Component By The Giving Key
        public T GetComponent<T>(ComponentKey componentKey) where T : IComponent
        {
            //int key = (int)componentKey;
            for (int i = 0, length = _components.Count; i < length; i++)
            {
                if (_components[i].Key == componentKey) return (T)_components[i];
            }
            //if (_components.TryGetValue(key, out IComponent component))
            //{
            //    return (T)component;
            //}
            DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
            return default;
        }
        public IComponent GetComponent(ComponentKey componentKey)
        {
            //int key = (int)componentKey;
            for (int i = 0, length = _components.Count; i < length; i++)
            {
                if (_components[i].Key == componentKey) return _components[i];
            }
            //if (_components.TryGetValue(key, out IComponent component))
            //{
            //    return component;
            //}
            DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
            return null;
        }

        //Add Component By The Giving Key
        //public void AddComponent(ComponentKey componentKey, IComponent component)
        public IEntity AddComponent(IComponent component)
        {
            //int key = (int)componentKey;
            for (int i = 0, length = _components.Count; i < length; i++)
            {
                if (_components[i].Key == component.Key)
                {
                    DebugClass.Log("Already have this component", DebugKey.Dotity);
                    return this;
                }
            }
            _components.Add(component);
            _onComponentAdded?.Invoke(this);
            return this;
            //if (_components.ContainsKey(key))
            //{
            //    DebugClass.Log("Already have this component", DebugKey.Dotity);
            //}
            //else
            //{
            //    _components.Add(key, component);
            //    _onComponentAdded?.Invoke(this);
            //}
        }

        //Remove Component By The Giving Key
        //Component Not Really Being Removed
        //It's Just Pushed Into Stack Reuse And Save For Later
        public void RemoveComponent(ComponentKey componentKey)
        {
            //int key = (int)componentKey;
            for (int i = 0, length = _components.Count; i < length; i++)
            {
                if (_components[i].Key == componentKey)
                {
                    Component.AddToReuseList((int)componentKey, _components[i]);
                    _components.RemoveAt(i);
                    _onComponentRemoved?.Invoke(this);
                    return;
                }
            }
            DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
            //if (_components.TryGetValue(key, out IComponent component))
            //{
            //    Component.AddToReuseList(key, component);
            //    _components.Remove(key);
            //    _onComponentRemoved?.Invoke(this);
            //}
            //else
            //{
            //    DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
            //}
        }
        private void RemoveComponent(int componentKey)
        {
            for (int i = 0, length = _components.Count; i < length; i++)
            {
                if (_components[i].Key == (ComponentKey)componentKey)
                {
                    Component.AddToReuseList(componentKey, _components[i]);
                    _components.RemoveAt(i);
                    _onComponentRemoved?.Invoke(this);
                    return;
                }
            }
            DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
            //if (_components.TryGetValue(componentKey, out IComponent component))
            //{
            //    Component.AddToReuseList(componentKey, component);
            //    _components.Remove(componentKey);
            //    _onComponentRemoved?.Invoke(this);
            //}
            //else
            //{
            //    DebugClass.Log("There is no componet suit with this key", DebugKey.Dotity);
            //}
        }

        public bool HasComponent(ComponentKey componentKey)
        {
            for (int i = 0, length = _components.Count; i < length; i++)
            {
                if (_components[i].Key == componentKey) return true;
            }
            return false;
        }
        //=> _components.ContainsKey((int)componentKey);
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
            for (int i = _components.Count; i >= 0; i++)
            {
                Component.AddToReuseList((int)_components[i].Key, _components[i]);
                _components.RemoveAt(i);
                _onComponentRemoved?.Invoke(this);
                //return;
            }
            //foreach (var item in _components.Keys)
            //{
            //    RemoveComponent(item);
            //}
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

