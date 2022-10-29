using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Arguments;
using HelixJump.Game.Interfaces;

namespace HelixJump.Game.Factories
{
    public class TowerLayerFactory
    {
        private readonly Dictionary<string, ITowerLayerCreator> _creators;

        public TowerLayerFactory(IEnumerable<ITowerLayerCreator> creators)
        {
            _creators = creators.ToDictionary(x => x.Type);
        }

        public ITowerLayer CreateTowerLayer(TowerLayerArguments towerLayerArguments)
        {
            var towerLayerType = towerLayerArguments.Type;
            if (!_creators.TryGetValue(towerLayerType, out var towerLayerCreator))
                throw new ApplicationException($"No tower layers with type {towerLayerType}");
            return _ = towerLayerCreator.Create(towerLayerArguments);
        }
    }
}