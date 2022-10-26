using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelixJump.Core.Enums;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Extensions;

namespace HelixJump.Core
{
    public class Player : IPlayer
    {
        private float _xPosition;

        private CancellationTokenSource _movingCancellationTokenSource = new ();
        
        private readonly TimeSpan _hitInterval;
        private readonly TimeSpan _movingInterval;
        
        public PlayerState State { get; private set; }

        public TaskCompletionSource<IHitInfo> PlayerHitTaskCompletionSource { get; } = new ();
        
        public int RowPosition => BaseTower.TowerLayers.Count();

        public ITower BaseTower { get; }

        public float XPosition
        {
            get => _xPosition;
            private set
            {
                if (value is > 1 or < 0)
                    throw new IndexOutOfRangeException("Player position cannot be bigger than 1f, or smaller than 0");

                _xPosition = value;
            }
        }

        public Player(ITower baseTower, TimeSpan hitInterval , TimeSpan movingInterval)
        {
            BaseTower = baseTower;
            _hitInterval = hitInterval;
            _movingInterval = movingInterval;
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
        
        public async void StartMoving()
        {
            const float movingStep = 0.001f;
            var cancellationToken = _movingCancellationTokenSource.Token;
            while (cancellationToken.IsCancellationRequested == false)
            {
                await Task.Delay(_movingInterval.Milliseconds);
                MoveForward(movingStep);
            }
        }
        
        public void StopMoving()
        {
            _movingCancellationTokenSource.Cancel();
            _movingCancellationTokenSource = new CancellationTokenSource();
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
        
        
        public void DisableHitMode()
        {
            State = PlayerState.Staying;
        }
        
        private bool TryHitTopTowerLayer()
        {
            if (BaseTower.GetTopTowerLayer(out var topTowerLayer) == false)
                return false;
            
            topTowerLayer.Hit(hittable: this, worldPosition: _xPosition);
            return true;
        }
        
        private void MoveForward(float position)
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


       
    }
}