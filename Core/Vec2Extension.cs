using Box2DX.Common;

namespace Core
{
    public static class Vec2Extension
    {
        public static Vec2 Normalize(Vec2 a) { return new Vec2(a.X / a.Length(), a.Y / a.Length()); }
        public static Vec2 LeftPerpendicular(Vec2 a) { return new Vec2(-a.Y, a.X); }
        public static Vec2 RightPerpendicular(Vec2 a) { return new Vec2(a.Y, -a.X); }
        public static Vec2 LeftNormal(Vec2 a) { return Normalize(LeftPerpendicular(a)); }
        public static Vec2 RightNormal(Vec2 a) { return Normalize(RightPerpendicular(a)); }

        public static Vec2 Divide(Vec2 a, float v) { return new Vec2(a.X / v, a.Y / v); }
        public static float Skalar(Vec2 a, Vec2 b) { return a.X * b.X + a.Y * b.Y; }


        public static float ScalarProjection(Vec2 a, Vec2 b) { return Skalar(a, b) / b.Length(); }
        public static Vec2 VectorProjection(Vec2 a, Vec2 b) { return (Skalar(a , b) / (b.LengthSquared())) * b; }
    }
}
