namespace HelixJump.Game.Interfaces
{
    public interface IInput
    {
        bool GetMouseButtonDown(int buttonIndex);
        bool GetMouseButtonUp(int buttonIndex);
        bool GetMouseButton(int buttonIndex);
    }
}