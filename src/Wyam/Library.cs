using System;
using System.Collections.Generic;
using System.Text;

namespace Wyam
{
    public partial class Library
    {
        /// <example>
        ///   <code>
        ///     public double Subtract(double a, double b)
        ///     {
        ///         return a* b;
        ///     }
        ///   </code>
        /// </example>
        /// Subtracts a double from another and returns the result
        /// Multiplies two intergers and returns the result
        /// See <see cref="Library.GetAnInt()"/> to get an Int.
        public double Subtract(double a, double b)
        {
            return a - b;
        }

        /// <example>
        ///   <code>
        ///     public static int Multiply(int a, int b)
        ///     {
        ///         return a* b;
        ///     }
        ///   </code>
        /// </example>
        /// <summary>
        /// Multiply Method
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        /// Multiplies two intergers and returns the result
        /// See <see cref="Library.GetAnInt()"/> to get an Int.
        public int Multiply(int a, int b)
        {
            return a * b;
        }

        /// <example>
        ///   <code>
        ///     GetAnInt(settings =>
        ///     {
        ///         settings.Quiet = true;
        ///     });
        ///   </code>
        /// </example>
        /// <summary>Writes the specified string value, followed by the current line terminator, to the standard output stream.</summary>
        /// <param name="value">The value to write.</param>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
        /// See <see cref="Library.Multiply(int, int)"/> to multiplay.
        public int GetAnInt()
        {
            return 5;
        }
    }
}
