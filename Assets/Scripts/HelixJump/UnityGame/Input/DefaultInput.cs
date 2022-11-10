using System.Threading;
using System.Threading.Tasks;

namespace HelixJump.UnityGame.Input
{
    public abstract class DefaultInput
    {
        private CancellationTokenSource _cancellationTokenSource = new();
        
        private void OnUpdate()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                OnLeftMouseButtonDown();
            if (UnityEngine.Input.GetMouseButtonUp(0))
                OnLeftMouseButtonUp();
            
        }

        protected virtual void OnLeftMouseButtonDown(){}
        protected virtual void OnLeftMouseButtonUp(){}

        public async void Enable()
        {
            var cancellationToken = _cancellationTokenSource.Token;

            while (cancellationToken.IsCancellationRequested == false)
            {
                await Task.Yield();
                OnUpdate();
            }
        }

        public void Disable()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}