using System;
using System.Collections.Generic;
using System.Linq;

namespace HelixJump.Game.Extensions
{
    public static class EnumerableExtensions
    {
        public static T SelectRandomItem<T>(this IEnumerable<T> enumerable)
        {
            var random = new Random();
            var array = enumerable.ToArray();
            var index = random.Next(0, array.Length - 1);
            return array[index];
        }
        
    }
}