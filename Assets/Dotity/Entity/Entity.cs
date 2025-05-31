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

        public Entity AddComponent<T>(IComponent component) where T : struct, IComponent
        {
            _componentIndexes.Add(typeof(T), ComponentPools.AddComponent<T>(component));
            _onComponentAdded?.Invoke(this);
            return this;
        }
        public void RemoveComponent<T>() where T : struct, IComponent
        {
            var index = _componentIndexes[typeof(T)];
            _componentIndexes.Remove(typeof(T));
            ComponentPools.RemoveComponent<T>(index);
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
            var method = typeof(Entity).GetMethod("RemoveComponent");
            foreach (Type type in _componentIndexes.Keys)
            {
                var generic = method.MakeGenericMethod(type);
                generic.Invoke(this, null);
            }
        }

        public void RegisterCallBackAddedComponent(Action<Entity> onEntityComponetAdded)
        {
            _onComponentAdded = onEntityComponetAdded;
        }

        public void RegisterCallBackRemovedComponent(Action<Entity> onEntityComponetRemoved)
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

