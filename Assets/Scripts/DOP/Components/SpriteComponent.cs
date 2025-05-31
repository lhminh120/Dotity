
using Dotity;

public struct SpriteComponent : IComponent
{
    private bool _hasChange;
    public bool IsChanged() => _hasChange;
    public void HasChanged()
    {
        if (!_hasChange) _hasChange = true;
    }
    public void FinishChanged() => _hasChange = false;
}
