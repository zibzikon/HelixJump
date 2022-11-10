using System.Collections.Generic;
using System.Linq;
using HelixJump.Game.Arguments;
using HelixJump.Game.Factories;
using HelixJump.Game.Interfaces.Arguments;
using UnityEngine;

namespace HelixJump.UnityGame.Arguments
{
    [CreateAssetMenu(fileName = "TowersArguments", menuName = "UnityGameArguments/TowersArguments")]
    public class TowersArguments : ScriptableObject, ITowersArguments
    {
        public TowerFactory TowerFactory { get; private set; }
        public IEnumerable<TowerArguments> TowerArgumentsCollection { get; private set; }

        [SerializeField] private List<UnityTowerArguments> towerArgumentsList;

        public void Initialize(TowerFactory towerFactory)
        {
            TowerArgumentsCollection = towerArgumentsList.Select(x => x.ToTowerArguments());
            TowerFactory = towerFactory;
        }
    }
}