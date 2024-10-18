using System;
using System.Collections.Generic;
using System.Text;

namespace _DRender2003
{
    class MathHelper
    {

        public const float PI = 3.14159265359f;

        public static float Sin(float value)
        {
            return (float)Math.Sin(value);
        }

        public static float Cos(float value)
        {
            return (float)Math.Cos(value);
        }

        public static float Tan(float value)
        {
            return (float)Math.Tan(value);
        }

        public static float ToRadians(float degrees)
        {
            return degrees * (float)Math.PI / 180f;
        }

        public static float Sqrt(float value)
        {
            return (float)Math.Sqrt(value); // Use Math.Sqrt and cast to float
        }
    }
}
