namespace HelixJump.Core.Utils
{
    public struct Rotation
    {
        public float Value { get; }

        public Rotation(float value)
        {
            Value = value;
        }

        public static Rotation operator +(Rotation left, Rotation right) => new Rotation(left.Value + right.Value);
        public static Rotation operator +(Rotation left, float right) => new Rotation(left.Value + right);
        
        public static Rotation operator -(Rotation left, Rotation right) => new Rotation(left.Value - right.Value);
        public static Rotation operator -(Rotation left, float right) => new Rotation(left.Value - right);
        
        public static Rotation operator *(Rotation left, Rotation right) => new Rotation(left.Value * right.Value);
        public static Rotation operator *(Rotation left, float right) => new Rotation(left.Value * right);
        
        public static Rotation operator /(Rotation left, Rotation right) => new Rotation(left.Value / right.Value);
        public static Rotation operator /(Rotation left, float right) => new Rotation(left.Value / right);

    }
}