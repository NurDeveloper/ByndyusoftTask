﻿using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.MathOperations
{
    /// <summary>
    /// The summation operation
    /// </summary>
    public class AddMathOperation : IMathOperation
    {
        public const string Keyword = "+";

        public int Priority => 1;

        public void Operate(Stack<double> stack)
        {
            if (stack != null && stack.Count() >= 2)
            {
                var result = stack.Pop();
                result = stack.Pop() + result;
                stack.Push(result);
            }
            else
            {
                throw new ArgumentException("Invalid mathematical expression.");
            }
        }
    }
}
