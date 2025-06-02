using System;
using System.Collections.Generic;
namespace Dotity
{
    public class ComponentPools
    {
        private static Dictionary<Type, IComponentPool> _pools = new();
        public static int AddComponent<T>(T component) where T : struct, IComponent
        {
            var type = typeof(T);
            if (_pools.TryGetValue(type, out var pool))
                return ((ComponentPool<T>)pool).AddComponent(component);
            pool = new ComponentPool<T>();
            _pools.Add(type, pool);
            return ((ComponentPool<T>)pool).AddComponent(component);
        }
        public static ref T GetComponent<T>(int index) where T : struct, IComponent
        {
            return ref ((ComponentPool<T>)_pools[typeof(T)]).GetComponent(index);
        }
        public static void RemoveComponent<T>(int index) where T : struct, IComponent
        {
            _pools[typeof(T)].RemoveComponent(index);
        }
        public static void RemoveComponent(Type type, int index)
        {
            _pools[type].RemoveComponent(index);
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
