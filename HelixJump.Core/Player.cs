using System;
using System.Threading.Tasks;
using HelixJump.Core.Extensions;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers.Layers;

namespace HelixJump.Core
{
    public class Player : IPlayer
    {
        public enum PlayerState
        {
            Staying,
            Hitting
        }
        
        public PlayerState State { get; private set; }
        
        public int RowPosition { get; private set; }

        private decimal _xPosition;

        public decimal XPosition
        {
            get => _xPosition;
            private set
            {
                if (value is > 1 or < 0)
                    throw new IndexOutOfRangeException("Player position cannot be bigger than 1f, or smaller than 0");

                _xPosition = value;
            }
        }

        public TaskCompletionSource<IHitInfo> PlayerHitTaskCompletionSource { get; } = new ();

        private readonly ITower _baseTower;
        private readonly TimeSpan _hitInterval;

        public Player(ITower baseTower, TimeSpan hitInterval)
        {
            _baseTower = baseTower;
            _hitInterval = hitInterval;
        }
        
        public void Hit(IHitInfo hitInfo)
        {
            Destroy();
            PlayerHitTaskCompletionSource.SetResult(hitInfo);
        }

        public void Destroy()
        {
            if (State == PlayerState.Hitting)
                DisableHitMode();
            
        }

        public void MoveForward(decimal position)
        {
            while (true)
            {
                var positionCapacity = 1 - _xPosition;
            
                if (position > positionCapacity)
                {
                    position -= positionCapacity;
                    _xPosition = 0;
                }
                else
                {
                    _xPosition += position;
                    break;
                }
            }
        }
        
        public async void EnableHitMode()
        {
            State = PlayerState.Hitting;
            while (State == PlayerState.Hitting)
            {
                await Task.Delay(_hitInterval.Milliseconds);
                if (TryHitTopTowerLayer() == false)
                    DisableHitMode();
            }
        }

        private bool TryHitTopTowerLayer()
        {
            var topTowerLayer = _baseTower.GetTopTowerLayer();
            if (topTowerLayer is  null)
                return false;
            
            topTowerLayer.Hit(hittable: this, worldPosition: _xPosition);
            return true;
        }

        public void DisableHitMode()
        {
            State = PlayerState.Staying;
        }
    }
}