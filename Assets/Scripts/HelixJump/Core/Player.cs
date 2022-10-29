using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HelixJump.Core.Enums;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using HelixJump.Game.Extensions;

namespace HelixJump.Core
{
    public class Player : IPlayer
    {
        
        public PlayerState State { get; private set; }

        public TaskCompletionSource<IHitInfo> PlayerHitTaskCompletionSource { get; } = new ();

        public Score Score { get; private set; }
        public ITower BaseTower { get; private set; }

        public Position Position 
        { 
            get => _position = new Position(BaseTower.Capacity.Value, _position.XPosition); 
            private set => _position = value; 
        }

        private CancellationTokenSource _movingCancellationTokenSource = new ();
        
        private readonly TimeSpan _hitInterval;
        private readonly TimeSpan _movingInterval;
        private Position _position;

        public Player(ITower baseTower, TimeSpan hitInterval , TimeSpan movingInterval)
        {
            BaseTower = baseTower;
            _hitInterval = hitInterval;
            _movingInterval = movingInterval;
        }
        
        public void Hit(IHitInfo hitInfo)
        {
            Destroy();
            PlayerHitTaskCompletionSource.TrySetResult(hitInfo);
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

        public void AddScore(int score)
        {
            Score = new Score(Score.Value + score);
        }

        public void RemoveScore(int score)
        {
            Score = new Score(Score.Value - score);
        }

        public void EnableHitMode()
        {
            State = PlayerState.Hitting;
            Task.Run(async () =>
            {
                while (State == PlayerState.Hitting)
                {
                    if (TryHitTopTowerLayer() == false)
                        DisableHitMode();
                    await Task.Delay(_hitInterval.Milliseconds);
                }
            });
        }
        
        public void DisableHitMode()
        {
            State = PlayerState.Staying;
        }

        public void ChangeBaseTower(ITower tower)
        {
            BaseTower = tower;
        }

        private bool TryHitTopTowerLayer()
        {
            if (BaseTower.GetTopTowerLayer(out var topTowerLayer) == false)
                return false;
            
            topTowerLayer.Hit(hittable: this, worldPosition: Position.XPosition);
            return true;
        }
        
        private void MoveForward(float position)
        {
            var xPosition = Position.XPosition;
            
            Position = new Position(Position.RowPosition, (xPosition+position).Clamp(0, 1));
        }


       
    }
}