using System;
using HelixJump.Core;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers;
using HelixJump.Core.Towers.Layers;
using HelixJump.Core.Towers.Layers.Parts;
using Xunit;

namespace HelixJump.Tests
{
    public class PlayerTests
    {
        [Fact]
        public void MoveForwardTests()
        {
            var resolution = new Resolution(2);
            var tower = new GameTower(new []{new TowerLayer(resolution, new Rotation(), new ITowerLayerPart[2]{new UnbreakableTowerLayerPart(), new WeaklyTowerLayerPart()})});
            var player = new Player(tower, new TimeSpan(0,0,0,0,500));
            
            player.MoveForward(2.34m);
            
            Assert.True(player.XPosition == 0.34m);
        }
    }
}