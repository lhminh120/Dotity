using System;
using System.Collections.Generic;

namespace Dotity
{
    public struct Entity
    {
        private struct ComponentIndexInfo
        {
            public ComponentKey key;
            public int index;
        }
        private List<ComponentIndexInfo> _componentIndexes;
        private bool _activeSelf;

        private Action<Entity> _onComponentAdded;
        private Action<Entity> _onComponentRemoved;
        public Entity Init()
        {
            _componentIndexes = new List<ComponentIndexInfo>();
            return this;
        }
        public void SetActive(bool active) => _activeSelf = active;
        public ref T GetComponent<T>(ComponentKey key) where T : struct, IComponent
        {
            for (int i = 0, length = _componentIndexes.Count; i < length; i++)
            {
                if (_componentIndexes[i].key == key)
                    return ref ComponentPools.GetComponent<T>(key, _componentIndexes[i].index);
            }
            return ref ComponentPools.GetComponent<T>(key, _componentIndexes[0].index); //this is not right thing, you should check HasComponent before using GetComponent
        }

        public Entity AddComponent<T>(T component) where T : struct, IComponent
        {
            _componentIndexes.Add(
                new ComponentIndexInfo()
                {
                    key = component.key,
                    index = ComponentPools.AddComponent(component)
                });
            _onComponentAdded?.Invoke(this);
            return this;
        }
        public Entity AddComponentWithoutNoti<T>(T component) where T : struct, IComponent
        {
            _componentIndexes.Add(
                new ComponentIndexInfo()
                {
                    key = component.key,
                    index = ComponentPools.AddComponent(component)
                });
            return this;
        }
        public void OnCompleteAddComponents()
        {
            _onComponentAdded?.Invoke(this);
        }
        public Entity RemoveComponent<T>(ComponentKey key) where T : struct, IComponent
        {
            for (int i = 0, length = _componentIndexes.Count; i < length; i++)
            {
                if (_componentIndexes[i].key == key)
                {
                    var index = _componentIndexes[i].index;
                    _componentIndexes.RemoveAt(i);
                    ComponentPools.RemoveComponent<T>(key, index);
                    _onComponentRemoved?.Invoke(this);
                    return this;
                }
            }
            return this;
        }
        public Entity RemoveComponentWithoutNoti<T>(ComponentKey key) where T : struct, IComponent
        {
            for (int i = 0, length = _componentIndexes.Count; i < length; i++)
            {
                if (_componentIndexes[i].key == key)
                {
                    var index = _componentIndexes[i].index;
                    _componentIndexes.RemoveAt(i);
                    ComponentPools.RemoveComponent<T>(key, index);
                    return this;
                }
            }
            return this;
        }
        public void OnCompleteRemoveComponents()
        {
            _onComponentRemoved?.Invoke(this);
        }

        public bool HasComponent(ComponentKey key)
        {
            for (int i = 0, length = _componentIndexes.Count; i < length; i++)
            {
                if (_componentIndexes[i].key == key)
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasComponents(params ComponentKey[] keys)
        {
            for (int i = 0, length = keys.Length; i < length; i++)
            {
                bool check = false;
                for (int j = 0, count = _componentIndexes.Count; j < count; j++)
                {
                    if (_componentIndexes[j].key == keys[i])
                    {
                        check = true;
                        break;
                    }
                }
                if (!check) return false;
            }
            return true;
        }

        //Remove All Components
        public void RemoveAllComponents()
        {
            for (int i = 0, length = _componentIndexes.Count; i < length; i++)
                ComponentPools.RemoveComponent(_componentIndexes[i].key, _componentIndexes[i].index);
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
    }
}

