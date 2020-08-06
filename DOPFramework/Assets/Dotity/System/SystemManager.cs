using Dotity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    private List<IInitializeSystem> _initializeSystems;
    private List<IExcuteSystem> _excuteSystems;
    private List<ICleanUpSystem> _cleanUpSystem;
}
