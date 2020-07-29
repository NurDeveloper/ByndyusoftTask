using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.MathOperations
{
    /// <summary>
    /// Division operation
    /// </summary>
    public class DivMathOperation : IMathOperation
    {
        public const string Keyword = "/";

        public int Priority => 2;

        public void Operate(Stack<double> stack)
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
    }
}
