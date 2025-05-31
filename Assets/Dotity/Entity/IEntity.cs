// using System;

// namespace Dotity
// {
//     public interface IEntity
//     {
//         void RegisterCallBackAddedComponent(Action<IEntity> onEntityComponentAdded);
//         void RegisterCallBackRemovedComponent(Action<IEntity> onEntityComponentRemoved);
//         void RemoveAllCallBack();
//         T GetComponent<T>() where T : IComponent;
//         //void AddComponent(ComponentKey componentKey, IComponent component);
//         //void AddComponent(IComponent component);
//         IEntity AddComponent<T>(T component) where T : IComponent;
//         bool HasComponent(Type type);
//         bool HasComponents(params Type[] types);
//         void RemoveComponent(Type type);
//         void RemoveAllComponents();
//     }
// }

