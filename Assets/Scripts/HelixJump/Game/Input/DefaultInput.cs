using HelixJump.Interfaces;

namespace HelixJump.Game.Input
{
    public abstract class DefaultInput : IUpdatable
    {
        public void OnUpdate()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                OnLeftMouseButtonDown();
            if (UnityEngine.Input.GetMouseButton(0))
                OnLeftMouseButton();
            if (UnityEngine.Input.GetMouseButtonUp(0))
                OnLeftMouseButtonUp();
            
            if (UnityEngine.Input.GetMouseButtonDown(1))
                OnRightMouseButtonDown();
            if (UnityEngine.Input.GetMouseButton(1))
                OnRightMouseButton();
            if (UnityEngine.Input.GetMouseButtonUp(1))
                OnRightMouseButtonUp();
        }

        protected virtual void OnLeftMouseButtonDown(){}
        protected virtual void OnLeftMouseButton(){}
        protected virtual void OnLeftMouseButtonUp(){}
        protected virtual void OnRightMouseButtonDown(){}
        protected virtual void OnRightMouseButton(){}
        protected virtual void OnRightMouseButtonUp(){}
    }
}