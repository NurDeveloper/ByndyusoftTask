using System;
using Calculator.Interfaces;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    /// Processor for performing mathematical operations
    /// </summary>
    public class MathProcessor : IMathProcessor
    {
        /// <summary>
        /// Execute operations in a mathematical expression
        /// </summary>
        /// <param name="expression">Mathematical expression</param>
        /// <returns>Execution result</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DivideByZeroException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public double Process(IEnumerable<object> expression)
        {
            var stack = new Stack<double>();

            foreach (var item in expression)
            {
                if (item is double)
                {
                    stack.Push((double)item);
                    continue;
                }

                if (item is IOperation operation)
                {
                    operation.Operate(stack);
                }
            }

            return stack.Peek();
        }
    }
}
