using System;
using System.Collections.Generic;

namespace Dotity
{
    public abstract class Component : IComponent
    {
        #region Static Function
        private static Dictionary<int, Stack<IComponent>> _listComponentsReuse = new Dictionary<int, Stack<IComponent>>();

        public static T Create<T>(int keyComponent) where T : new()
        {
            return (_listComponentsReuse.TryGetValue(keyComponent, out Stack<IComponent> components) && components.Count > 0) ? (T)components.Pop() : new T();
        }
        public static void AddToReuseList(int componentKey, IComponent component)
        {
            if (_listComponentsReuse.TryGetValue(componentKey, out Stack<IComponent> components))
            {
                components.Push(component);
            }
            else
            {
                Stack<IComponent> _tempStack = new Stack<IComponent>();
                _tempStack.Push(component);
                _listComponentsReuse.Add(componentKey, _tempStack);
            }
        }

        
        #endregion
        #region Function
        private bool _hasChange = false;
        public bool IsChange() => _hasChange;
        public void HasChange() => _hasChange = true;
        public void FinishChange() => _hasChange = false;
        public abstract ComponentKey Key { get; }
        #endregion
    }
}

