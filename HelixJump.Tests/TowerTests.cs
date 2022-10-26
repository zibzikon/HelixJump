using System.Collections.Generic;
using System.Threading.Tasks;
using HelixJump.Core;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers;
using HelixJump.Core.Towers.Layers;
using HelixJump.Core.Towers.Layers.Parts;
using Xunit;

namespace HelixJump.Tests
{
    public class TowerTests
    {
        [Fact]
        public async void TowerDestroyingTest()
        {
            var towerLayerParts = new ITowerLayerPart[] { new WeaklyTowerLayerPart()};
            var towerLayerParts2 = new ITowerLayerPart[] { new WeaklyTowerLayerPart()};
            var towerLayerParts3 = new ITowerLayerPart[] { new WeaklyTowerLayerPart()};
            
            var towerLayers = new ITowerLayer[] {new DefaultTowerLayer(new Resolution(1), new Rotation(), towerLayerParts)};
            var towerLayers2 = new ITowerLayer[] {new DefaultTowerLayer(new Resolution(1), new Rotation(), towerLayerParts2)};
            var towerLayers3 = new ITowerLayer[] {new DefaultTowerLayer(new Resolution(1), new Rotation(), towerLayerParts3)};
            
            var tower = new GameTower(towerLayers);
            var tower2 = new GameTower(towerLayers2);
            var tower3 = new GameTower(towerLayers3);
            var taskCompletionSource = new TaskCompletionSource<ITower>();

            var destroyedTowersList = new List<ITower>(3);

            OnTowerDestroyed(destroyedTowersList, tower, taskCompletionSource);
            OnTowerDestroyed(destroyedTowersList, tower2, taskCompletionSource);
            OnTowerDestroyed(destroyedTowersList, tower3, taskCompletionSource);
            
            tower.Destroy(); 
            towerLayers2[0].Destroy();
            towerLayerParts3[0].Destroy();

            await taskCompletionSource.Task;
            
            Assert.True(destroyedTowersList.Count == 3);
            Assert.Contains(tower, destroyedTowersList);
            Assert.Contains(tower2, destroyedTowersList);
            Assert.Contains(tower3, destroyedTowersList);
            
            static async void OnTowerDestroyed(List<ITower> destroyedTowers, ITower tower, TaskCompletionSource<ITower> taskCompletionSource)
            {
                await tower.DestroyedTaskCompletionSource.Task;
                destroyedTowers.Add(tower);
                if (destroyedTowers.Count ==3)
                    taskCompletionSource.SetResult(tower);
            }
        }
        
        
    }
}