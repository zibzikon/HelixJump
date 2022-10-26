using System.Threading.Tasks;

namespace HelixJump.Interfaces
{
    public interface IObjectPoolObject
    {

        TaskCompletionSource<IObjectPoolObject> DisabledTaskCompletionSource { get; }
        TaskCompletionSource<IObjectPoolObject> EnabledTaskCompletionSource { get; }
        void Disable();
        void Enable();
    }
}