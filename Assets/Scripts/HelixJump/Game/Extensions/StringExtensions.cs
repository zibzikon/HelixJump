using System;

namespace HelixJump.Game.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt32(this string s) => Int32.Parse(s);
        public static float ToFloat(this string s) => Single.Parse(s);
    }
}