using System;

namespace Dotity
{
    public interface IEntity
    {
        void RegisteCallBackAddedComponent(Action<IEntity> onEntityComponetAdded);
        void RegisteCallBackRemovedComponent(Action<IEntity> onEntityComponetRemoved);
        void RemoveAllCallBack();
        IComponent GetComponent(ComponentKey componentKey);
        void AddComponent(ComponentKey componentKey, IComponent component);
        bool HasComponent(ComponentKey componentKey);
        bool HasComponents(ComponentKey[] componentKeys);
        void RemoveComponent(ComponentKey componentKey);
        void RemoveAllComponents();
    }
}

