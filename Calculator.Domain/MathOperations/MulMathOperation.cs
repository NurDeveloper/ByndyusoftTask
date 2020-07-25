using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Domain.MathOperations
{
    /// <summary>
    /// Multiplication operation
    /// </summary>
    public class MulMathOperation : MathOperation
    {
        public const string Keyword = "*";

        public override int Priority => 2;

        public override void Operate(Stack<double> stack)
        {
            if (stack != null && stack.Count() >= 2)
            {
                var result = stack.Pop();
                result = stack.Pop() * result;
                stack.Push(result);
            }
            else
            {
                throw new ArgumentException("Invalid mathematical expression.");
            }
        }

        public override string ToString()
        {
            return "*";
        }
    }
}
