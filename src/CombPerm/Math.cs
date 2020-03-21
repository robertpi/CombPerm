using System;
using System.Collections.Generic;
using System.Text;

namespace CombPerm
{
    public static class Math
    {
        /// <summary>
        /// calculates the factorial of the given input
        /// </summary>
        /// <param name="n">given input</param>
        /// <returns>the factorial</returns>
        public static int Factorial(int n) 
        {
            // suprised this didn't exist already, but seems it doesn't
            int fact = n;
            for (int i = n - 1; i >= 1; i--)
            {
                fact *= i;
            }
            return fact;
        }
    }
}
