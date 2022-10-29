using System;
using System.Threading.Tasks;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

namespace HelixJump.Debug.Tests
{
    public class TaskCompletionSourceTests : MonoBehaviour
    {
        [SerializeField] private bool _do;

        TaskCompletionSourceContainer _testContainer = new (); 
        
        private void Start()
        {
            var subscribersCount = 10;
            for (int i = 0; i < subscribersCount; i++)
            {
                var i1 = i;
                var subscriber = TaskCompletionSourceSubscriber.Subscribe(_testContainer.Task,
                    () => UnityEngine.Debug.Log(i1.ToString()));
            }
            TestAwaiting();
        }

        private async void TestAwaiting()
        {
            await _testContainer.Task;
            UnityEngine.Debug.Log("Test awaiting");
        }
        private void Update()
        {
            if (!_do) return;
            _testContainer.TrySetResult();
            _do = false;
        }
    }
    
    
    internal class TaskCompletionSourceSubscriber
    {
        private TaskCompletionSourceSubscriber(Task task, Action actionAfterAwaiting)
        {
            StartAwaitingTask(task, actionAfterAwaiting);
        }
        
        public static TaskCompletionSourceSubscriber Subscribe(Task task, Action actionAfterAwaiting)
        {
            return new TaskCompletionSourceSubscriber(task, actionAfterAwaiting);
        }

        private async void StartAwaitingTask(Task task, Action actionAfterAwaiting)
        {
            await task;
            actionAfterAwaiting();
        }
    }
    
    
    internal class TaskCompletionSourceContainer
    {
        private readonly TaskCompletionSource<bool> _taskCompletionSource = new();

        public Task Task => _taskCompletionSource.Task;
        
        public bool TrySetResult()
            =>_taskCompletionSource.TrySetResult(true);
        
    }
}