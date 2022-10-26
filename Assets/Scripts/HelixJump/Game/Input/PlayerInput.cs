using System;
using System.Threading.Tasks;
using HelixJump.Core.Interfaces;

namespace HelixJump.Game.Input
{
    public class PlayerInput : DefaultInput
    {
        private readonly IPlayer _player;

        public PlayerInput(IPlayer player)
        {
            _player = player;
        }
        
        protected override void OnLeftMouseButtonDown()
        {
            EnablePlayerHitMode();
        }

        protected override void OnLeftMouseButtonUp()
        {
            DisablePlayerHitMode();
        }

        private void EnablePlayerHitMode()
        {
            _player.EnableHitMode();
        }

        private void DisablePlayerHitMode()
        {
            _player.DisableHitMode();
        }
    }
}