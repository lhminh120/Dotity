using Dotity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private SystemManager _systemManager = new SystemManager();
    public static float _tick = 0.01667f;
    private void Awake()
    {
        InitSystem();
        _systemManager.Initialize();
        //StartCoroutine(WaitForOneSecond());
    }
    private void Update()
    {
        _tick = Time.deltaTime;
        _systemManager.Excute();
        _systemManager.Render();
        _systemManager.CleanUp();
    }
    private void InitSystem()
    {
        _systemManager
            .Add(new CreateViewSystem())
            .Add(new MoveSystem())
            .Add(new RenderTransformSystem());
    }
    IEnumerator WaitForOneSecond()
    {
        while (true)
        {
            DebugClass.Log("One Second Count");
            _systemManager.Excute();
            _systemManager.Render();
            _systemManager.CleanUp();
            yield return new WaitForSeconds(1);
        }
    }
}
