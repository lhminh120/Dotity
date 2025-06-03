using System;
using System.Collections.Generic;
namespace Dotity
{
    public class ComponentPools
    {
        private struct ComponentPoolInfo
        {
            public ComponentKey key;
            public IComponentPool pool;
        }
        private static List<ComponentPoolInfo> _pools = new();
        public static int AddComponent<T>(T component) where T : struct, IComponent
        {
            for (int i = 0, length = _pools.Count; i < length; i++)
            {
                if (_pools[i].key == component.key)
                    return ((ComponentPool<T>)_pools[i].pool).AddComponent(component);
            }
            var pool = new ComponentPool<T>();
            _pools.Add(
                new ComponentPoolInfo()
                {
                    key = component.key,
                    pool = pool,
                }
            );
            return pool.AddComponent(component);
        }
        public static ref T GetComponent<T>(ComponentKey key, int index) where T : struct, IComponent
        {
            for (int i = 0, length = _pools.Count; i < length; i++)
            {
                if (_pools[i].key == key)
                    return ref ((ComponentPool<T>)_pools[i].pool).GetComponent(index);
            }
            return ref ((ComponentPool<T>)_pools[0].pool).GetComponent(index); // this is not right thing, you should check HasComponent before using GetComponent
        }
        public static void RemoveComponent<T>(ComponentKey key, int index) where T : struct, IComponent
        {
            for (int i = 0, length = _pools.Count; i < length; i++)
            {
                if (_pools[i].key == key)
                    _pools[i].pool.RemoveComponent(index);
            }
        }
        public static void RemoveComponent(ComponentKey key, int index)
        {
            for (int i = 0, length = _pools.Count; i < length; i++)
            {
                if (_pools[i].key == key)
                    _pools[i].pool.RemoveComponent(index);
            }
        }
    }
    public interface IComponentPool
    {
        void RemoveComponent(int index);
    }
    public class ComponentPool<T> : IComponentPool where T : struct, IComponent
    {
        private T[] _components = new T[128];
        private Stack<int> _emptyIndexes = new();
        private int _count = 0;

        public int AddComponent(T component)
        {
            if (_emptyIndexes.Count > 0)
            {
                int emptyIndex = _emptyIndexes.Pop();
                _components[emptyIndex] = component;
                return emptyIndex;
            }
            if (_count >= _components.Length)
                Array.Resize(ref _components, _components.Length * 2);

            int index = _count++;
            _components[index] = component;
            return index;
        }
        public void RemoveComponent(int index)
        {
            _emptyIndexes.Push(index);
        }

        public ref T GetComponent(int index) => ref _components[index];
    }

}
