using System;
using Xunit;

namespace HelixJump.Tests
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            TowerLayer towerLayer = null;
            try
            {
                towerLayer = new TowerLayer(new Resolution(5), new ITowerLayerPart[5]);
            }
            catch 
            {
            }
            
            Assert.True(towerLayer != null);
        }
    }
}