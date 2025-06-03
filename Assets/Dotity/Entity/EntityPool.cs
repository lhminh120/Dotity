using System.Collections.Generic;

namespace Dotity
{
    public class EntityPool
    {
        public static List<Entity> entities = new List<Entity>();
        public static Entity CreateEntity()
        {
            Entity entity = new Entity();
            entity.RegisterCallBackAddedComponent(Group.OnEntityAddComponent);
            entity.RegisterCallBackRemovedComponent(Group.OnEntityRemoveComponent);
            entities.Add(entity);
            return entity;
        }
        public static void DesTroyEntity(Entity entity)
        {
            entity.RemoveAllComponents();
            entity.RemoveAllCallBack();
            entities.Remove(entity);
        }
    }
}