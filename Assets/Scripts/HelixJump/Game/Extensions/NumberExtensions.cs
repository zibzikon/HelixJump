namespace HelixJump.Game.Extensions
{
    public static class NumberExtensions
    {
        public static float Clamp(this float number,float leftBorder, float rightBorder)
        {
            var clumpedNumber = leftBorder;
            var multiplier = 1;
            if (leftBorder > rightBorder)
                multiplier = -1;
            
            while (true)
            {
                var rotationCapacity = rightBorder - clumpedNumber;
            
                if (leftBorder > rightBorder? number < rotationCapacity: number > rotationCapacity)
                {
                    number += rotationCapacity * -multiplier;
                    clumpedNumber = 0;
                }
                else
                {
                    clumpedNumber += number;
                    break;
                }
            }
            
            return clumpedNumber;
        }

        public static bool InRange(this int number, float leftBorder, float rightBorder) =>
            number >= leftBorder && number <= rightBorder;
    }
}