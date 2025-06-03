using System;
using System.Collections.Generic;

namespace Dotity
{
    public class Entity : IEntity
    {
        #region Static Function
        private static readonly Stack<IEntity> _entitiesReuse = new();
        public static List<IEntity> _entities = new();
        public static Entity CreateEntity()
        {
            Entity entity = null;
            if (_entitiesReuse.Count > 0)
                entity = (Entity)_entitiesReuse.Pop();
            else
            {
                entity = new Entity();
                entity.Id = _entities.Count;
                _entities.Add(entity);
            }
            entity.RegisterCallBackAddedComponent(Group.OnEntityAddComponent);
            entity.RegisterCallBackRemovedComponent(Group.OnEntityRemoveComponent);
            return entity;
        }
        public static void AddToReuseList(IEntity entity)
        {
            _entitiesReuse.Push(entity);
        }

        /// <summary>
        /// Entity Is Not Really Destroyed, It's Just Added To Reuse List
        /// </summary>
        /// <param name="entity"></param>
        public static void DesTroyEntity(IEntity entity)
        {
            entity.RemoveAllComponents();
            entity.RemoveAllCallBack();
            IEntity temp = entity;
            AddToReuseList(temp);
        }
        #endregion
        #region Function
        private readonly List<IComponent> _components = new List<IComponent>();
        private bool _activeSelf = true;

        private Action<IEntity> _onComponentAdded;
        private Action<IEntity> _onComponentRemoved;

        public void SetActive(bool active) => _activeSelf = active;
        public bool IsActive => _activeSelf;
        private int _id;
        public int Id { get => _id; set => _id = value; }

        /// <summary>
        /// Get Component By The Giving Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="componentKey"></param>
        /// <returns></returns>
        public T GetComponent<T>(ComponentKey componentKey) where T : IComponent
        {
            for (int i = 0, length = _components.Count; i < length; i++)
            {
                if (_components[i].Key == componentKey) return (T)_components[i];
            }
            DebugClass.Log("There is no component suit with this key", DebugKey.Dotity);
            return default;
        }

        /// <summary>
        /// Add Component By The Giving Key
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public IEntity AddComponent(IComponent component)
        {
            if (HasComponent(component.Key))
                return this;
            _components.Add(component);
            _onComponentAdded?.Invoke(this);
            return this;
        }
        /// <summary>
        /// Add Component By The Giving Key
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public IEntity AddComponents(params IComponent[] components)
        {
            for (int i = 0, length = components.Length; i < length; i++)
            {
                if (!HasComponent(components[i].Key))
                    _components.Add(components[i]);
            }
            _onComponentAdded?.Invoke(this);
            return this;
        }

        /// <summary>
        /// Remove Component By The Giving Key, Component Not Really Being Removed, It's Just Pushed Into Stack Reuse And Save For Later
        /// </summary>
        /// <param name="componentKey"></param>
        public void RemoveComponent(ComponentKey componentKey)
        {
            for (int i = 0, length = _components.Count; i < length; i++)
            {
                if (_components[i].Key == componentKey)
                {
                    Component.AddToReuseList(componentKey, _components[i]);
                    _components.RemoveAt(i);
                    _onComponentRemoved?.Invoke(this);
                    return;
                }
            }
            DebugClass.Log("There is no component suit with this key", DebugKey.Dotity);
        }

        public bool HasComponent(ComponentKey componentKey)
        {
            for (int i = 0, length = _components.Count; i < length; i++)
            {
                if (_components[i].Key == componentKey) return true;
            }
            return false;
        }
        public bool HasComponents(ComponentKey[] componentKeys)
        {
            for (int i = 0, length = componentKeys.Length; i < length; i++)
            {
                if (!HasComponent(componentKeys[i])) return false;
            }
            return true;
        }


        /// <summary>
        /// Remove All Components 
        /// </summary>
        public void RemoveAllComponents()
        {
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                Component.AddToReuseList(_components[i].Key, _components[i]);
                _components.RemoveAt(i);
                _onComponentRemoved?.Invoke(this);
            }
        }

        public void RegisterCallBackAddedComponent(Action<IEntity> onEntityComponentAdded)
        {
            _onComponentAdded = onEntityComponentAdded;
        }

        public void RegisterCallBackRemovedComponent(Action<IEntity> onEntityComponentRemoved)
        {
            _onComponentRemoved = onEntityComponentRemoved;
        }

        public void RemoveAllCallBack()
        {
            _onComponentAdded = null;
            _onComponentRemoved = null;
        }
        #endregion
    }
}

