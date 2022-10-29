using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers.Layers;

namespace HelixJump.Game.Extensions
{
    public static class TowerLayerExtensions
    {
        public static int GetTowerLayerPartPositionByWorldXPosition(this ITowerLayer towerLayer, float position)
        {
            const float oneTurn = 360f;
            var correctPosition = 0;
            var oneTowerLayerPartRotationScaler = oneTurn / towerLayer.Resolution.Value;
            var rotation = position * oneTurn;
            var nextTowerLayerRotation = oneTowerLayerPartRotationScaler;
            for (var i = 0; i < towerLayer.Resolution.Value; i++)
            {
                if (rotation -towerLayer.Rotation.Value <= nextTowerLayerRotation)
                {
                    correctPosition = i;
                    break;
                }
                nextTowerLayerRotation += oneTowerLayerPartRotationScaler;
            }

            return correctPosition;
        }

        public static void Hit(this ITowerLayer towerLayer, IHittable hittable, float worldPosition)
        {
            var position = towerLayer.GetTowerLayerPartPositionByWorldXPosition(worldPosition);
            towerLayer.Hit(new TowerLayerHitInfo(position, hittable));
        }
    }
}