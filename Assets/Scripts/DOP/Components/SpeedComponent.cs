
using Dotity;

public struct SpeedComponent : IComponent
{
    public SpeedComponent(float speed)
    {
        _speed = speed;
        _hasChange = false;
    }
    public float _speed;
    private bool _hasChange;
    public bool IsChanged() => _hasChange;
    public void HasChanged()
    {
        if (!_hasChange) _hasChange = true;
    }
    public void FinishChanged() => _hasChange = false;
}
