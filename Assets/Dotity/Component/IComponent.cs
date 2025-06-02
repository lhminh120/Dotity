namespace Dotity
{
    public interface IComponent
    {
        ComponentKey key { get; }
        bool IsChanged();
        void HasChanged();
        void FinishChanged();
    }
}

