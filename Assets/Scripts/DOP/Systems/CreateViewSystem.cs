using System;
using System.Diagnostics;
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

        EntityPool.CreateEntity()
            .Init()
            .AddComponentWithoutNoti(new GameObjectComponent(obj))
            .AddComponentWithoutNoti(new PositionComponent(trans.position))
            .AddComponentWithoutNoti(new TransformComponent(trans))
            .AddComponentWithoutNoti(new SpeedComponent(1))
            .OnCompleteAddComponents();
    }
}
