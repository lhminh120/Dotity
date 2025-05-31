using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dotity
{
    public class ComponentPools
    {
        private static Dictionary<Type, IComponentPool> _pools = new();
        public static int AddComponent<T>(IComponent component) where T : struct, IComponent
        {
            var type = typeof(T);
            if (_pools.TryGetValue(type, out var pool))
            {
                return pool.AddComponent(component);
            }
            var poolType = typeof(ComponentPool<>).MakeGenericType(type);
            pool = (IComponentPool)Activator.CreateInstance(poolType);
            _pools.Add(type, pool);
            return pool.AddComponent(component);
        }
        public static ref T GetComponent<T>(int index) where T : struct, IComponent
        {
            return ref ((ComponentPool<T>)_pools[typeof(T)]).GetComponent(index);
        }
        public static void RemoveComponent<T>(int index) where T : struct, IComponent
        {
            ((ComponentPool<T>)_pools[typeof(T)]).RemoveComponent(index);
        }
    }
    public interface IComponentPool
    {
        int AddComponent(IComponent component);
    }
    public class ComponentPool<T> : IComponentPool where T : struct, IComponent
    {
        private T[] _components = new T[128];
        private Stack<int> _emptyIndexes = new();
        private int _count = 0;

        public int AddComponent(IComponent component)
        {
            if (_emptyIndexes.Count > 0)
            {
                int emptyIndex = _emptyIndexes.Pop();
                _components[emptyIndex] = (T)component;
                return emptyIndex;
            }
            if (_count >= _components.Length)
                Array.Resize(ref _components, _components.Length * 2);

            int index = _count++;
            _components[index] = (T)component;
            return index;
        }
        public void RemoveComponent(int index)
        {
            _emptyIndexes.Push(index);
        }

        public ref T GetComponent(int index) => ref _components[index];
    }

}
