using System;

namespace Dotity
{
    public interface IEntity
    {
        void RegisteCallBackAddedComponent(Action<IEntity> onEntityComponetAdded);
        void RegisteCallBackRemovedComponent(Action<IEntity> onEntityComponetRemoved);
        void RemoveAllCallBack();
        T GetComponent<T>(ComponentKey componentKey) where T : IComponent;
        //void AddComponent(ComponentKey componentKey, IComponent component);
        //void AddComponent(IComponent component);
        IEntity AddComponent(IComponent component);
        bool HasComponent(ComponentKey componentKey);
        bool HasComponents(ComponentKey[] componentKeys);
        void RemoveComponent(ComponentKey componentKey);
        void RemoveAllComponents();
    }
}

