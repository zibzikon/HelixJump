using System.Threading.Tasks;
using HelixJump.Game.Interfaces;
using UnityEngine;

namespace HelixJump.GameVIew.Views
{
    public abstract class ViewBase : MonoBehaviour, IGameObject
    {
        public TaskCompletionSource<IGameObject> ResetAndDisabledTaskCompletionSource { get; private set; }

        public Task<IGameObject> ResetAndDisabledTask => ResetAndDisabledTaskCompletionSource.Task;

        protected void Initialize()
        {
            ResetAndDisabledTaskCompletionSource = new TaskCompletionSource<IGameObject>();
        }
        
        public IGameObject Instantiate()
            => Instantiate(this);

        public virtual void ResetAndDisable()
        {
            gameObject.SetActive(false);
            ResetAndDisabledTaskCompletionSource.SetResult(this);
            ResetAndDisabledTaskCompletionSource = new TaskCompletionSource<IGameObject>();
        }
    }
}