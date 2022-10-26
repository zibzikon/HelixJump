using System;
using HelixJump.Arguments;
using Xunit;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HelixJump.Tests
{
    public class DeserializationTests
    {
        [Fact]
        public void TowerLayerDeserializationTest()
        {
            var yaml = @"
type: default
resolution: 6
parts:
  - type: unbreakable
  - type: unbreakable
  - type: unbreakable
  - type: weakly
  - type: weakly
  - type: weakly";

            TowerLayerArguments towerLayerArguments = null;
            try
            {
                towerLayerArguments = Deserialize<TowerLayerArguments>(yaml);
            }
            catch (Exception e)
            {
                towerLayerArguments = null;
            }

            Assert.NotNull(towerLayerArguments);

        }

        [Fact]
        public void TowerDeserializationTest()
        {
            var yaml = @"
 towerLayers:
    - type: default
      resolution: 6
      parts:
         - type: unbreakable
         - type: unbreakable
         - type: unbreakable
         - type: weakly
         - type: weakly
         - type: weakly";

            TowerArguments towerArguments = null;
            try
            {
                towerArguments = Deserialize<TowerArguments>(yaml);
            }
            catch (Exception e)
            {
                towerArguments = null;
            }

            Assert.NotNull(towerArguments);
        }

        private T Deserialize<T>(string yaml)
        {
            var deserializer = new DeserializerBuilder().
                WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            
            return deserializer.Deserialize<T>(yaml);
        }
    }
}