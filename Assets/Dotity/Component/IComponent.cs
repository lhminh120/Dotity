namespace Dotity
{
    public interface IComponent
    {
        bool IsChanged();
        void HasChanged();
        void FinishChanged();
        // ComponentKey Key { get; }
    }
}

