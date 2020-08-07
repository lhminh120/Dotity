using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebugKey
{
    None,
    Default,
    Dotity
}
public class DebugClass : MonoBehaviour
{
    private static bool _showAll = true;
    private static DebugKey _debugKeyLog = DebugKey.Default;
    private static DebugKey _debugKeyLogError = DebugKey.Default;
    private static DebugKey _debugKeyLogWarning = DebugKey.Default;
    public static void Log(object obj, DebugKey debugKey = DebugKey.Default)
    {
        if(debugKey == _debugKeyLog || _showAll)
        {
            Debug.Log(obj);
        }
    }
    public static void LogError(object obj, DebugKey debugKey = DebugKey.Default)
    {
        if (debugKey == _debugKeyLogError || _showAll)
        {
            Debug.LogError(obj);
        }
    }
    public static void LogWarning(object obj, DebugKey debugKey = DebugKey.Default)
    {
        if (debugKey == _debugKeyLogWarning || _showAll)
        {
            Debug.LogWarning(obj);
        }
    }
}
