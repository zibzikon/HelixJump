using System.Collections.Generic;
using System.Linq;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Towers.Layers.Parts;
using HelixJump.Game.Arguments;
using HelixJump.Game.Factories;
using HelixJump.Game.Interfaces.Creators.Tower;

namespace HelixJump.Game.Creators
{
    public abstract class BaseTowerLayerPartsCreator : ITowerLayerPartCreator
    {
        private readonly TowerLayerPartModifierFactory _towerLayerPartModifierFactory;
        public abstract string Type { get; }
        
        protected BaseTowerLayerPartsCreator(TowerLayerPartModifierFactory towerLayerPartModifierFactory)
        {
            _towerLayerPartModifierFactory = towerLayerPartModifierFactory;
        }

        protected IEnumerable<ITowerLayerPartModifier> GenerateTowerLayerPartModifiers(TowerLayerPartArguments towerLayerPartArguments)
        {
            return towerLayerPartArguments.Modifiers.Select(args =>
                _towerLayerPartModifierFactory.CreateTowerLayerPartModifier(args));
        }

        public abstract ITowerLayerPart Create(TowerLayerPartArguments towerLayerPartArguments);

    }
    
    public class UnbreakableLayerPartsCreator : BaseTowerLayerPartsCreator
    {
        public override string Type => "unbreakable";
        
        
        public UnbreakableLayerPartsCreator(TowerLayerPartModifierFactory towerLayerPartModifierFactory) : base(towerLayerPartModifierFactory) { }
        
        public override ITowerLayerPart Create(TowerLayerPartArguments towerLayerPartArguments)
        {
            return new UnbreakableTowerLayerPart(GenerateTowerLayerPartModifiers(towerLayerPartArguments));
        }

    }

    public class WeaklyLayerPartsCreator : BaseTowerLayerPartsCreator
    {
        public override string Type => "weakly";
        
        public WeaklyLayerPartsCreator(TowerLayerPartModifierFactory towerLayerPartModifierFactory) : base(towerLayerPartModifierFactory) { }

        public override ITowerLayerPart Create(TowerLayerPartArguments towerLayerPartArguments)
        {
            return new WeaklyTowerLayerPart(GenerateTowerLayerPartModifiers(towerLayerPartArguments));
        }
    }
}