using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Domain.MathOperations
{
    /// <summary>
    /// Division operation
    /// </summary>
    public class DivMathOperation : MathOperation
    {
        public const string Keyword = "/";

        public override int Priority => 2;

        public override void Operate(Stack<double> stack)
        {
            if (stack != null && stack.Count() >= 2)
            {
                var result = stack.Pop();

                if (result == 0)
                {
                    throw new DivideByZeroException("You cannot divide by zero.");
                }

                result = stack.Pop() / result;

                stack.Push(result);
            }
            else
            {
                throw new ArgumentException("Invalid mathematical expression.");
            }
        }

        public override string ToString()
        {
            return "/";
        }
    }
}
