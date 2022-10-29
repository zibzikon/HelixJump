namespace HelixJump.Core.Utils
{
    public struct IntRange
    {
        public int Left { get; }
        public int Right { get; }
        
        public IntRange(int left, int right)
        {
            Left = left;
            Right = right;
        }
    }
}