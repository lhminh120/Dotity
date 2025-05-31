using System.Collections.Generic;

namespace Dotity
{
    public class EntityPool
    {
        // private static readonly Stack<IEntity> _entitiesReuse = new Stack<IEntity>();
        public static List<Entity> entities = new List<Entity>();
        public static Entity CreateEntity()
        {
            Entity entity = new Entity();
            entity.RegisterCallBackAddedComponent(Group.OnEntityAddComponent);
            entity.RegisterCallBackRemovedComponent(Group.OnEntityRemoveComponent);
            entities.Add(entity);
            // UnityEngine.Debug.Log("entity pool " + entities.Count);
            return entity;
        }
        //Entity Is Not Really Destroyed, It's Just Added To Reuse List
        public static void DesTroyEntity(Entity entity)
        {
            entity.RemoveAllComponents();
            entity.RemoveAllCallBack();
            entities.Remove(entity);
        }
    }
}