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
        //StartCoroutine(WaitForOneSecond());
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
        _systemManager.Add(new RenderTransformSystem());
    }
    IEnumerator WaitForOneSecond()
    {
        while (true)
        {
            _systemManager.Excute();
            _systemManager.CleanUp();
            yield return new WaitForSeconds(1);
        }
    }
}
