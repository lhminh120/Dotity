using Dotity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GameSystem
{
    protected override void InitSystem()
    {
        _systemManager
            .Add(new CreateViewSystem())
            .Add(new MoveSystem())
            .Add(new RenderTransformSystem());
    }
}
