namespace Dotity
{
    public interface IComponent
    {
        bool IsChange();
        void HasChange();
        void FinishChange();
        ComponentKey Key { get; }
    }
}

