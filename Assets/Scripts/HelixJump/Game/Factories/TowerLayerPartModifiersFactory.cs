using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Game.Arguments;
using HelixJump.Game.Interfaces.Creators.Tower;

namespace HelixJump.Game.Factories
{
    public class TowerLayerPartModifierFactory
    {
        private readonly Dictionary<string, ITowerLayerPartModifiersCreator> _creators;

        public TowerLayerPartModifierFactory(IEnumerable<ITowerLayerPartModifiersCreator> creators)
        {
            _creators = creators?.ToDictionary(x => x.Type);
        }
        
        public ITowerLayerPartModifier CreateTowerLayerPartModifier(TowerLayerPartModifierArguments arguments)
        {
            var type = arguments.Type;
            if (!_creators.TryGetValue(type, out var creator))
                throw new ApplicationException($"No tower layer part creator with type {type}");

            return creator.Create();
        } 
    }
}