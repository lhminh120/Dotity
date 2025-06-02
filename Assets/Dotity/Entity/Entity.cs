using System;
using System.Collections.Generic;

namespace Dotity
{
    public struct Entity
    {
        #region Static Function

        #endregion
        #region Function
        private Dictionary<ComponentKey, int> _componentIndexes;
        private bool _activeSelf;

        private Action<Entity> _onComponentAdded;
        private Action<Entity> _onComponentRemoved;
        public Entity Init()
        {
            _componentIndexes = new Dictionary<ComponentKey, int>();
            return this;
        }
        public void SetActive(bool active) => _activeSelf = active;
        public ref T GetComponent<T>(ComponentKey key) where T : struct, IComponent
        {
            return ref ComponentPools.GetComponent<T>(key, _componentIndexes[key]);
        }

        public Entity AddComponent<T>(T component) where T : struct, IComponent
        {
            _componentIndexes.Add(component.key, ComponentPools.AddComponent(component));
            _onComponentAdded?.Invoke(this);
            return this;
        }
        public Entity AddComponentWithoutNoti<T>(T component) where T : struct, IComponent
        {
            _componentIndexes.Add(component.key, ComponentPools.AddComponent(component));
            return this;
        }
        public void OnCompleteAddComponents()
        {
            _onComponentAdded?.Invoke(this);
        }
        public Entity RemoveComponent<T>(ComponentKey key) where T : struct, IComponent
        {
            var index = _componentIndexes[key];
            _componentIndexes.Remove(key);
            ComponentPools.RemoveComponent<T>(key, index);
            _onComponentRemoved?.Invoke(this);
            return this;
        }
        public Entity RemoveComponentWithoutNoti<T>(ComponentKey key) where T : struct, IComponent
        {
            var index = _componentIndexes[key];
            _componentIndexes.Remove(key);
            ComponentPools.RemoveComponent<T>(key, index);
            return this;
        }
        public void OnCompleteRemoveComponents()
        {
            _onComponentRemoved?.Invoke(this);
        }

        public bool HasComponent(ComponentKey key)
        {
            if (_componentIndexes.TryGetValue(key, out var componentIndex))
            {
                return true;
            }
            return false;
        }
        public bool HasComponents(params ComponentKey[] keys)
        {
            for (int i = 0, length = keys.Length; i < length; i++)
            {
                if (!_componentIndexes.TryGetValue(keys[i], out var componentIndex))
                {
                    return false;
                }
            }
            return true;
        }

        //Remove All Components
        public void RemoveAllComponents()
        {
            var keys = new List<ComponentKey>(_componentIndexes.Keys);
            for (int i = 0, length = keys.Count; i < length; i++)
                ComponentPools.RemoveComponent(keys[i], _componentIndexes[keys[i]]);
            _componentIndexes.Clear();
            _onComponentRemoved?.Invoke(this);
        }

        public void RegisterCallBackAddedComponent(Action<Entity> onEntityComponentAdded)
        {
            _onComponentAdded = onEntityComponentAdded;
        }

        public void RegisterCallBackRemovedComponent(Action<Entity> onEntityComponentRemoved)
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

