using System.Threading.Tasks;
using HelixJump.Interfaces;
using UnityEngine;

namespace HelixJump.GamePlay.Views
{
    public class ViewBase : MonoBehaviour, IObjectPoolObject
    {
        public TaskCompletionSource<IObjectPoolObject> DisabledTaskCompletionSource { get; private set; } = new();
        public TaskCompletionSource<IObjectPoolObject> EnabledTaskCompletionSource { get; private set;} = new();

        public virtual void Enable()
        {
            gameObject.SetActive(true);
            EnabledTaskCompletionSource.SetResult(this);
            EnabledTaskCompletionSource = new TaskCompletionSource<IObjectPoolObject>();
        }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
            DisabledTaskCompletionSource.SetResult(this);
            DisabledTaskCompletionSource = new TaskCompletionSource<IObjectPoolObject>();
        }

        public virtual void ResetAndDisable()
        {
            Disable();
        }
    }
}