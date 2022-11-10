using System;
using System.Threading;
using System.Threading.Tasks;

namespace HelixJump.Game.Extensions
{
    public static class TaskExtensions
    {
        public static async void OnTaskEnded<T> (Func<Task<T>> getTaskFunc, Action action, CancellationToken cancellationToken)
        {
            await getTaskFunc.Invoke();
            
            if (cancellationToken.IsCancellationRequested)
                return;
            
            action();
            
            OnTaskEnded(getTaskFunc, action, cancellationToken);
        }
    }
}