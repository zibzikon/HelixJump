using HelixJump.Game.Interfaces.Arguments;
using UnityEngine;

namespace HelixJump.UnityGame.Arguments
{
    [CreateAssetMenu(fileName = "UnityPlayerArguments", menuName = "UnityGameArguments/UnityPlayerArguments")]
    public class UnityPlayerArguments : ScriptableObject, IPlayerArguments
    {
        public int HitInterval => hitInterval;
        public int MovingInterval => movingInterval;

        [SerializeField] private int hitInterval;
        [SerializeField] private int movingInterval;
    }
}