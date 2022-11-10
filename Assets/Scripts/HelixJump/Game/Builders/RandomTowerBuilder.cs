using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using HelixJump.Game.Arguments;
using HelixJump.Game.Extensions;
using HelixJump.Game.Factories;
using HelixJump.Game.Interfaces.Builders.Tower;

namespace HelixJump.Game.Builders
{
    public class RandomTowerBuilder : ITowerBuilder
    {
        private readonly IEnumerable<TowerArguments> _towersArguments;
        private readonly TowerFactory _towerFactory;
        public RandomTowerBuilder(TowerFactory towerFactory, IEnumerable<TowerArguments> towersArguments)
        {
            _towerFactory = towerFactory;
            _towersArguments = towersArguments;
        }

        public ITower Build(int difficulty)
        {
            var towerArgumentsCollection = 
                SelectRandomTowersByDifficulty(_towersArguments.ToArray(), difficulty);

            var towerArguments = CombineTowerArgumentsCollection(towerArgumentsCollection);
            return _ = _towerFactory.Get(towerArguments);
        }

        private TowerArguments CombineTowerArgumentsCollection(IEnumerable<TowerArguments> towerArgumentsCollection)
        {
            var sumDifficulty = 0;
            var towerLayerArgumentsList = new List<TowerLayerArguments>();
            var sumCapacity = 0;
            var towerArgumentsEnumerable = towerArgumentsCollection as TowerArguments[] ?? towerArgumentsCollection.ToArray();
            var type = towerArgumentsEnumerable.Select(x => x.Type).Distinct().SelectRandomItem() 
                       ?? throw new ArgumentNullException();

            var towerLayers = towerArgumentsEnumerable.Where(x => x.Type == type).ToArray();
            foreach (var towerArguments in towerLayers)
            {
                sumCapacity += towerArguments.Capacity.ToInt32();
                sumDifficulty += towerArguments.Difficulty.ToInt32();
                towerLayerArgumentsList.AddRange(towerArguments.TowerLayers);
            }
            
            var layerRotationStep = towerLayers.Select(x => x.LayerRotationStep.ToFloat()).Sum() / towerLayers.Count();

            return new TowerArguments()
            {
                TowerLayers = towerLayerArgumentsList,
                Capacity = sumCapacity.ToString(),
                Difficulty = sumDifficulty.ToString(),
                LayerRotationStep = layerRotationStep.ToString(CultureInfo.InvariantCulture),
                Type = type
            };
        }
        
        private IEnumerable<TowerArguments> SelectRandomTowersByDifficulty(TowerArguments[] towersArguments, int difficulty)
        {
            var random = new Random();
            var sumDifficulty = 0;
            var allowableDifficultyError = new IntRange(-5, 5);

            while (sumDifficulty < difficulty)
            {
                var index = random.Next(0, towersArguments.Length - 1);
                var tower = towersArguments[index];
                var sum = sumDifficulty + Int32.Parse(tower.Difficulty);

                if (sum.InRange(allowableDifficultyError.Left, allowableDifficultyError.Right) == false)
                    continue;
                
                sumDifficulty = sum;
                
                yield return tower;
            }
        }
    }
}