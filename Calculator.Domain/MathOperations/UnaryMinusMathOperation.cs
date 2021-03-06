﻿using Calculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Domain.MathOperations
{
    /// <summary>
    /// Unary minus operation
    /// </summary>
    public class UnaryMinusMathOperation : MathOperation
    {
        public const string Keyword = "~";

        public override int Priority => 1;

        public override OperationType Type => OperationType.Unary;

        public override void Operate(Stack<double> stack)
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
