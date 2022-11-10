using System;
using System.Threading.Tasks;
using HelixJump.Game.Interfaces;
namespace HelixJump.UnityGame.Input
{
    public class PlayerInput : DefaultInput, IPlayerInput
    {
        public event Action EnableHitMode;
        public event Action DisableHitMode;

        protected override void OnLeftMouseButtonDown()
        {
            EnableHitMode?.Invoke();
        }

        protected override void OnLeftMouseButtonUp()   
        {
            DisableHitMode?.Invoke();
        }
        
    }
}