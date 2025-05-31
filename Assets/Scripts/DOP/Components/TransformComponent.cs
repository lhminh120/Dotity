
using Dotity;
using UnityEngine;

public struct TransformComponent : IComponent
{
    public TransformComponent(Transform transform)
    {
        _transform = transform;
        _hasChange = false;
    }
    public Transform _transform;
    private bool _hasChange;
    public bool IsChanged() => _hasChange;
    public void HasChanged()
    {
        if (!_hasChange) _hasChange = true;
    }
    public void FinishChanged() => _hasChange = false;
}
