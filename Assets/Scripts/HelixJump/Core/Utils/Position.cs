using System;

namespace HelixJump.Core.Utils
{
    public struct Position
    {
        public readonly float XPosition;
        public readonly int RowPosition;

        public Position(int rowPosition, float xPosition)
        {
            if (xPosition is > 1 or < 0)
                throw new IndexOutOfRangeException("xPosition cannot be bigger than 1f, or smaller than 0");
            
            RowPosition = rowPosition;
            XPosition = xPosition;
        }
    }
}