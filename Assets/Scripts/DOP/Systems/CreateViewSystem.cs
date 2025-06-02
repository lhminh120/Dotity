using Dotity;
using UnityEngine;

public class CreateViewSystem : IInitializeSystem
{
    private int numberEntities = 10000;
    public void Initialize()
    {
        for (int i = 0; i < numberEntities; i++)
        {
            CreateEntity();
        }
    }
    private void CreateEntity()
    {
        GameObject obj = GameData.Instance.CreateObject();
        Transform trans = obj.transform;
        //IEntity entity = Entity.CreateEntity();
        //entity.AddComponent(ComponentKey.GameObject, new GameObjectComponent(obj));
        //entity.AddComponent(ComponentKey.Position, new PositionComponent(new Point3D(trans.position)));
        //entity.AddComponent(ComponentKey.Transform, new TransformComponent(trans));
        //entity.AddComponent(ComponentKey.Speed, new SpeedComponent(1));

        //entity.AddComponent(new GameObjectComponent(obj));
        //entity.AddComponent(new PositionComponent(new Point3D(trans.position)));
        //entity.AddComponent(new TransformComponent(trans));
        //entity.AddComponent(new SpeedComponent(1));

        EntityPool.CreateEntity()
            .Init()
            .AddComponentWithoutNoti(new GameObjectComponent(obj))
            .AddComponentWithoutNoti(new PositionComponent(trans.position))
            .AddComponentWithoutNoti(new TransformComponent(trans))
            .AddComponentWithoutNoti(new SpeedComponent(1))
            .OnCompleteAddComponents();
    }
}
