using Dotity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SystemManager _systemManager = new SystemManager();
    private void Awake()
    {
        InitSystem();
        _systemManager.Initialize();
    }
    private void Update()
    {
        _systemManager.Excute();
        _systemManager.CleanUp();
    }
    private void InitSystem()
    {
        _systemManager.Add(new CreateViewSystem());
        _systemManager.Add(new MoveSystem());
    }
    
}
