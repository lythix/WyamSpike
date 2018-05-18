namespace Wyam
{
    public interface ILibrary
    {
        int GetAnInt();
        int Multiply(int a, int b);

        /// <example>
        ///   <code>
        ///     public double Subtract(double a, double b)
        ///     {
        ///         return a* b;
        ///     }
        ///   </code>
        /// </example>
        /// Interface example is here Subtracts a double from another and returns the result
        /// Multiplies two intergers and returns the result
        /// See <see cref="Library.GetAnInt()"/> to get an Int.
        double Subtract(double a, double b);
    }
}