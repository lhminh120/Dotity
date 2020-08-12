using Dotity;
using UnityEngine;

public class CreateViewSystem : IInitializeSystem
{
    private int numberEntities = 2000;
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
        IEntity entity = Entity.CreateEntity();
        entity.AddComponent(ComponentKey.GameObject, new GameObjectComponent(obj));
        entity.AddComponent(ComponentKey.Position, new PositionComponent(new Point3D(trans.position)));
        entity.AddComponent(ComponentKey.Transform, new TransformComponent(trans));
        entity.AddComponent(ComponentKey.Speed, new SpeedComponent(1));
    }
}
