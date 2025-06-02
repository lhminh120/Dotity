namespace Dotity
{
    public interface IComponent
    {
        bool IsChanged();
        void HasChanged();
        void FinishChanged();
    }
}

