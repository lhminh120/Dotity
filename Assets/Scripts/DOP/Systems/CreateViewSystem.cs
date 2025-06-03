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
        GameObject obj = Singleton<GameData>.Instance.CreateObject();
        Transform trans = obj.transform;

        Entity.CreateEntity()
            .AddComponents(
                Dotity.Component.Create<GameObjectComponent>(ComponentKey.GameObject).Init(obj),
                Dotity.Component.Create<PositionComponent>(ComponentKey.Position).Init(trans.position),
                Dotity.Component.Create<TransformComponent>(ComponentKey.Transform).Init(trans),
                Dotity.Component.Create<SpeedComponent>(ComponentKey.Speed).Init(1));
    }
}
