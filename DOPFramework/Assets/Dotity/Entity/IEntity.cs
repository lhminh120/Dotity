namespace Dotity
{
    public interface IEntity
    {
        IComponent GetComponent(ComponentKey componentKey);
        void AddComponent(ComponentKey componentKey, IComponent component);
        bool HasComponent(ComponentKey componentKey);
        bool HasComponents(ComponentKey[] componentKeys);
        void RemoveComponent(ComponentKey componentKey);
        void RemoveComponent(short componentKey);
        void RemoveAllComponents();
    }
}

