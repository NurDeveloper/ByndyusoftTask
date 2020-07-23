using System.Collections.Generic;

namespace Calculator.Domain.MathOperations
{
    /// <summary>
    /// Mathematical operation abstract class
    /// </summary>
    public abstract class MathOperation
    {
        /// <summary>
        /// Priority of math operation
        /// </summary>
        public abstract int Priority { get; }

        /// <summary>
        /// Execute operation
        /// </summary>
        /// <param name="stack">Stack of arguments for the operation</param>
        public abstract void Operate(Stack<double> stack);
    }
}
