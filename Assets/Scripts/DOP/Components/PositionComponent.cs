
using Dotity;
using UnityEngine;

public struct PositionComponent : IComponent
{
    public PositionComponent(Vector3 position)
    {
        _position = position;
        _hasChange = false;
    }
    public Vector3 _position;
    private bool _hasChange;
    public bool IsChanged() => _hasChange;
    public void HasChanged()
    {
        if (!_hasChange) _hasChange = true;
    }
    public void FinishChanged() => _hasChange = false;
}
