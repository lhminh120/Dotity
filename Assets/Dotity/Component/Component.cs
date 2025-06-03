
using System.Collections.Generic;

namespace Dotity
{
    public abstract class Component : IComponent
    {
        #region Static Function
        private static readonly Dictionary<ComponentKey, Stack<IComponent>> _listComponentsReuse = new();

        public static T Create<T>(ComponentKey keyComponent) where T : IComponent, new()
        {
            return (_listComponentsReuse.TryGetValue(keyComponent, out Stack<IComponent> components) && components.Count > 0) ? (T)components.Pop() : new T();
        }
        public static void AddToReuseList(ComponentKey componentKey, IComponent component)
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
        public void HasChange()
        {
            if (!_hasChange) _hasChange = true;
        }
        public void FinishChange() => _hasChange = false;
        public abstract ComponentKey Key { get; }
        #endregion
    }
}

