using System.Collections.Generic;

namespace Calculator.MathOperations
{
    /// <summary>
    /// Operation execution interface
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Execute operation
        /// </summary>
        /// <param name="stack">Stack of arguments for the operation</param>
        void Operate(Stack<double> stack);
    }
}