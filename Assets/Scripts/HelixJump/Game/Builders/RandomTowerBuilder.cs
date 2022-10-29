using System;
using System.Collections.Generic;
using System.Linq;
using HelixJump.Core.Interfaces.Tower;
using HelixJump.Core.Utils;
using HelixJump.Game.Arguments;
using HelixJump.Game.Extensions;
using HelixJump.Game.Factories;
using HelixJump.Game.Interfaces;

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
            return _towerFactory.Get(towerArguments);
        }

        private TowerArguments CombineTowerArgumentsCollection(IEnumerable<TowerArguments> towerArgumentsCollection)
        {
            var sumDifficulty = 0;
            var towerLayerArgumentsList = new List<TowerLayerArguments>();
            foreach (var towerArguments in towerArgumentsCollection)
            {
                sumDifficulty += Int32.Parse(towerArguments.Difficulty);
                towerLayerArgumentsList.AddRange(towerArguments.TowerLayers);
            }

            return _ = new TowerArguments() { TowerLayers = towerLayerArgumentsList, Difficulty = sumDifficulty.ToString() };
        }
        
        private IEnumerable<TowerArguments> SelectRandomTowersByDifficulty(TowerArguments[] towersArguments, int difficulty)
        {
            var random = new Random();
            var sumDifficulty = 0;
            var allowableDifficultyError = new IntRange(-5, 5);

            while (sumDifficulty < difficulty)
            {
                var index = random.Next(0, towersArguments.Count());
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