using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.MathOperations
{
    /// <summary>
    /// Unary minus operation
    /// </summary>
    public class UnaryMinusMathOperation : IMathOperation
    {
        public string Keyword => "~";

        public int Priority => 1;

        public void Operate(Stack<double> stack)
        {
            if (stack != null && stack.Count() >= 1)
            {
                var result = stack.Pop();
                result = -result;
                stack.Push(result);
            }
            else
            {
                throw new ArgumentException("Invalid mathematical expression.");
            }
        }
    }
}
