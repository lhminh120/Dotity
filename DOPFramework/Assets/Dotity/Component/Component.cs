using System.Collections.Generic;
namespace Dotity
{
    public class Component : IComponent
    {
        #region Static Function
        private static Dictionary<short, Stack<IComponent>> _listComponentsReuse = new Dictionary<short, Stack<IComponent>>();
        public static T Create<T>(short keyComponent) where T : new()
        {
            return (_listComponentsReuse.ContainsKey(keyComponent) && _listComponentsReuse[keyComponent].Count > 0) ? (T)_listComponentsReuse[keyComponent].Pop() : new T();
        }
        public static void AddToReuseList(short componentKey, IComponent component)
        {
            if (_listComponentsReuse.ContainsKey(componentKey))
            {
                _listComponentsReuse[componentKey].Push(component);
            }
            else
            {
                Stack<IComponent> _tempStack = new Stack<IComponent>();
                _tempStack.Push(component);
                _listComponentsReuse.Add(componentKey, _tempStack);
            }
        }
        #endregion
    }
}

