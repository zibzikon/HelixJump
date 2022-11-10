using System.Threading.Tasks;
using HelixJump.Game.Interfaces;
using UnityEngine;

namespace HelixJump.GameView.Views
{
    public abstract class ViewBase : MonoBehaviour, IGameObject
    {
        private TaskCompletionSource<IGameObject> _resetAndDisabledTaskCompletionSource = new ();

        public Task<IGameObject> ResetAndDisabledTask => _resetAndDisabledTaskCompletionSource.Task;
        
        public bool Disabled { get; private set; }

        public void Enable()
        {
            gameObject.SetActive(true);
        }
        
        public IGameObject Instantiate()
            => Instantiate(this);

        public virtual void ResetAndDisable()
        {
            gameObject.SetActive(false);
            
            Disabled = true;
            _resetAndDisabledTaskCompletionSource.TrySetResult(this);
            _resetAndDisabledTaskCompletionSource = new TaskCompletionSource<IGameObject>();
        }
    }
}