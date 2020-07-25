﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Domain.MathOperations
{
    /// <summary>
    /// The summation operation
    /// </summary>
    public class AddMathOperation : MathOperation
    {
        public const string Keyword = "+";

        public override int Priority => 1;

        public override void Operate(Stack<double> stack)
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
