namespace HelixJump.Core.Interfaces
{
    public interface IPause
    {
        bool Paused { get; }
        void Pause();
        void Resume();
    }
}