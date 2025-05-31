
using Dotity;
using UnityEngine;

public struct GameObjectComponent : IComponent
{
    public GameObjectComponent(GameObject gameObject)
    {
        _obj = gameObject;
        _hasChange = false;
    }
    public GameObject _obj;
    private bool _hasChange;
    public bool IsChanged() => _hasChange;
    public void HasChanged()
    {
        if (!_hasChange) _hasChange = true;
    }
    public void FinishChanged() => _hasChange = false;

}
