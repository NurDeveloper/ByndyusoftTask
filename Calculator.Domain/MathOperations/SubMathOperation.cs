using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Domain.MathOperations
{
    /// <summary>
    /// Subtraction operation
    /// </summary>
    public class SubMathOperation : MathOperation
    {
        public override int Priority => 1;

        public override void Operate(Stack<double> stack)
        {
            if (stack != null && stack.Count() >= 2)
            {
                var result = stack.Pop();
                result = stack.Pop() - result;
                stack.Push(result);
            }
            else
            {
                throw new ArgumentException("Invalid mathematical expression.");
            }
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
