using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelixJump.Core;
using HelixJump.Core.Interfaces;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers.Layers;
using Xunit;

namespace HelixJump.Tests
{
    public class TowerLayerTests
    {
        [Fact]
        public void TowerLayerConstructionTest()
        {
            TowerLayer towerLayer = null;
            TowerLayer towerLayer2 = null;
            
            try { towerLayer = new TowerLayer(new Resolution(5), new Rotation(), new ITowerLayerPart[5]); }
            catch(Exception ex) { towerLayer = null; }

            try { towerLayer2 = new TowerLayer(new Resolution(0), new Rotation(), new ITowerLayerPart[0]); }
            catch (Exception ex) { towerLayer2 = null; }
            
            Assert.True(towerLayer == null);
            Assert.True(towerLayer2 != null);
        }

        [Fact]
        public async void TowerLayerDestroyingTests()
        {
            var towerLayer = new TowerLayer(new Resolution(0), new Rotation(), new ITowerLayerPart[0]);
            var callbacksList = new List<int>();
            int ft = 1, sc = 2, td = 3, fs = 4;
            OnTowerLayerDestroyed(ft);
            OnTowerLayerDestroyed(sc);
            OnTowerLayerDestroyed(td);
            OnTowerLayerDestroyed(fs);
            towerLayer.Destroy();
            
            await Task.Delay(500);
            
            Assert.True(callbacksList.Count == 4);
            
            Assert.True(callbacksList.Contains(ft));
            Assert.True(callbacksList.Contains(sc));
            Assert.True(callbacksList.Contains(td));
            Assert.True(callbacksList.Contains(fs));
            
            async void OnTowerLayerDestroyed(int callbackNumber)
            {
                await towerLayer.DestroyedTaskCompletionSource.Task;
                callbacksList.Add(callbackNumber);
            }
        }
    }
}