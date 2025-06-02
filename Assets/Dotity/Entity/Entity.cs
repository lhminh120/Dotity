using System;
using System.Collections.Generic;

namespace Dotity
{
    public struct Entity
    {
        #region Static Function

        #endregion
        #region Function
        private Dictionary<Type, int> _componentIndexes;
        private bool _activeSelf;

        private Action<Entity> _onComponentAdded;
        private Action<Entity> _onComponentRemoved;
        public Entity Init()
        {
            _componentIndexes = new Dictionary<Type, int>();
            return this;
        }
        public void SetActive(bool active) => _activeSelf = active;
        public ref T GetComponent<T>() where T : struct, IComponent
        {
            return ref ComponentPools.GetComponent<T>(_componentIndexes[typeof(T)]);
        }

        public Entity AddComponent<T>(T component) where T : struct, IComponent
        {
            _componentIndexes.Add(typeof(T), ComponentPools.AddComponent(component));
            _onComponentAdded?.Invoke(this);
            return this;
        }
        public Entity AddComponentWithoutNoti<T>(T component) where T : struct, IComponent
        {
            _componentIndexes.Add(typeof(T), ComponentPools.AddComponent(component));
            return this;
        }
        public void OnCompleteAddComponents()
        {
            _onComponentAdded?.Invoke(this);
        }
        public Entity RemoveComponent<T>() where T : struct, IComponent
        {
            var index = _componentIndexes[typeof(T)];
            _componentIndexes.Remove(typeof(T));
            ComponentPools.RemoveComponent<T>(index);
            _onComponentRemoved?.Invoke(this);
            return this;
        }
        public Entity RemoveComponentWithoutNoti<T>() where T : struct, IComponent
        {
            var index = _componentIndexes[typeof(T)];
            _componentIndexes.Remove(typeof(T));
            ComponentPools.RemoveComponent<T>(index);
            return this;
        }
        public void OnCompleteRemoveComponents()
        {
            _onComponentRemoved?.Invoke(this);
        }

        public bool HasComponent(Type type)
        {
            if (_componentIndexes.TryGetValue(type, out var componentIndex))
            {
                return true;
            }
            return false;
        }
        public bool HasComponents(params Type[] types)
        {
            for (int i = 0, length = types.Length; i < length; i++)
            {
                if (!_componentIndexes.TryGetValue(types[i], out var componentIndex))
                {
                    return false;
                }
            }
            return true;
        }

        //Remove All Components
        public void RemoveAllComponents()
        {
            var keys = new List<Type>(_componentIndexes.Keys);
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

