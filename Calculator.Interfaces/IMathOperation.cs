using System.Collections.Generic;

namespace Calculator.Interfaces
{
    /// <summary>
    /// Mathematical operation interface
    /// </summary>
    public interface IMathOperation
    {
        /// <summary>
        /// Priority of math operation
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Execute operation
        /// </summary>
        /// <param name="stack">Stack of arguments for the operation</param>
        void Operate(Stack<double> stack);
    }
}