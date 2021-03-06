﻿using System;
using Simple.Patterns;
using Simple.Common;

namespace Simple
{
    /// <summary>
    /// Contains helper methods for working with simple math operations.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Apply the real modular operation, returning values in range [0, q).
        /// </summary>
        /// <param name="p">The dividend.</param>
        /// <param name="q">The divisor.</param>
        /// <returns>The modulo q result.</returns>
        public static long RealMod(this long p, long q)
        {
            long r = p % q;
            if (r < 0) return r + q;
            return r;
        }


        /// <summary>
        /// Rounds an integer number to the first less than or equal number that is 0 modulo q.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="q"></param>
        /// <returns>The rounded number.</returns>
        public static long ModRound(this long p, long q)
        {
            return p - RealMod(p, q);
        }
        /// <summary>
        /// Rounds an integer number to the first less than or equal number that is 0 modulo q.
        /// </summary>
        /// <param name="p">The number.</param>
        /// <param name="q">The modular set.</param>
        /// <returns>The rounded number.</returns>
        public static int ModRound(this int p, int q)
        {
            return (int)(p - RealMod(p, q));
        }

        /// <summary>
        /// Swaps two variables.
        /// </summary>
        /// <typeparam name="T">The variables type.</typeparam>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }

        /// <summary>
        /// Compares two decimal values for equality inside some delta.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <param name="delta">The delta value.</param>
        /// <returns>True if they are almost equal, false otherwise.</returns>
        public static bool FloatEq(this decimal a, decimal b, decimal delta)
        {
            return Math.Abs(a - b) < delta;
        }

        /// <summary>
        /// Compares two float values for equality inside some delta.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <param name="delta">The delta value.</param>
        /// <returns>True if they are almost equal, false otherwise.</returns>
        public static bool FloatEq(this float a, float b, float delta)
        {
            return Math.Abs(a - b) < delta;
        }

        /// <summary>
        /// Compares two double values for equality inside some delta.
        /// </summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <param name="delta">The delta value.</param>
        /// <returns>True if they are almost equal, false otherwise.</returns>
        public static bool FloatEq(this double a, double b, double delta)
        {
            return Math.Abs(a - b) < delta;
        }


        static class Internal
        {
            public static PrimeNumbers Instance = new PrimeNumbers();
        }

        public static PrimeNumbers GetPrimes()
        {
            return Internal.Instance;
        }


    }
}
