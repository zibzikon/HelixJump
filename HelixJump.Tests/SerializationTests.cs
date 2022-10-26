using System.Collections.Generic;
using HelixJump.Arguments;
using Xunit;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HelixJump.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void TowerArgumentsSerializationTest()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var towerArguments = new TowerArguments()
            {
                TowerLayers = new List<TowerLayerArguments>()
                {
                    new TowerLayerArguments()
                    {
                        Parts = new List<TowerLayerPartArguments>()
                        {
                            new() { Type = "unbreakable" },
                            new() { Type = "unbreakable" },
                            new() { Type = "unbreakable" },
                            new() { Type = "weakly" },
                            new() { Type = "weakly" },
                            new() { Type = "weakly" },
                        },
                        Resolution = "6",
                        Type = "default"
                    }
                }
            };

            var yamlString = serializer.Serialize(towerArguments);

            Assert.Equal(@"towerLayers:
- type: default
  resolution: 6
  rotation: 
  parts:
  - type: unbreakable
    modifiers: 
  - type: unbreakable
    modifiers: 
  - type: unbreakable
    modifiers: 
  - type: weakly
    modifiers: 
  - type: weakly
    modifiers: 
  - type: weakly
    modifiers: 
", yamlString);
        }
    }
}