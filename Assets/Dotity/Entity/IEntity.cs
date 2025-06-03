using System;

namespace Dotity
{
    public interface IEntity
    {
        void RegisterCallBackAddedComponent(Action<IEntity> onEntityComponentAdded);
        void RegisterCallBackRemovedComponent(Action<IEntity> onEntityComponentRemoved);
        void RemoveAllCallBack();
        T GetComponent<T>(ComponentKey componentKey) where T : IComponent;
        IEntity AddComponent(IComponent component);
        IEntity AddComponents(params IComponent[] components);
        bool HasComponent(ComponentKey componentKey);
        bool HasComponents(ComponentKey[] componentKeys);
        void RemoveComponent(ComponentKey componentKey);
        void RemoveAllComponents();
    }
}

