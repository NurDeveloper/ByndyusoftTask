﻿using System;
using Calculator.Interfaces;
using System.Collections.Generic;
using Calculator.Domain.ExpressionUnits;
using System.Globalization;
using Calculator.Domain.MathOperations;

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
        /// <param name="mathOperationsContainer">Avaliable math operations</param>
        /// <returns>Execution result</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="DivideByZeroException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public double Process(IEnumerable<ExpressionUnit> expression, IMathOperationsContainer mathOperationsContainer)
        {
            var stack = new Stack<double>();

            foreach (var item in expression)
            {
                if (item.Type == Domain.Enums.ExpressionUnitType.Number)
                {
                    stack.Push(double.Parse(item.Value, NumberStyles.Any, CultureInfo.InvariantCulture));
                    continue;
                }

                if (item.Type == Domain.Enums.ExpressionUnitType.Operation)
                {
                    var operation = CreateOperationByKeyword(item, mathOperationsContainer);

                    operation.Operate(stack);
                }
            }

            return stack.Peek();
        }

        private static MathOperation CreateOperationByKeyword(ExpressionUnit item, IMathOperationsContainer mathOperationsContainer)
        {
            var operation = mathOperationsContainer.GetOperationOrDefault(item.Value);

            if (operation == null)
            {
                throw new ArgumentException("Invalid mathematical expression or unsupported operation.");
            }

            return operation;
        }
    }
}
