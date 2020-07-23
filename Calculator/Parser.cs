﻿using Calculator.Domain.ExpressionUnits;
using Calculator.Domain.MathOperations;
using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Calculator
{
    /// <summary>
    /// Parser of mathematical expression string
    /// </summary>
    public class Parser : IParser
    {
        /// <summary>
        /// Parse string to terms of mathematical expression
        /// </summary>
        /// <param name="expression">Input expression</param>
        /// <param name="mathOperations">Avaliable math operations</param>
        /// <returns>Parsed mathematical expression</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public IEnumerable<ExpressionUnit> Parse(string expression, MathOperation[] mathOperations)
        {
            var result = new List<ExpressionUnit>();

            var valueBuilder = new StringBuilder();

            for (var i = 0; i < expression.Length; i++)
            {
                if (TryParseMathOperation(expression[i].ToString(), mathOperations, out ExpressionUnit mathOperation))
                {
                    ParseValue(result, valueBuilder);

                    result.Add(mathOperation);

                    continue;
                }

                valueBuilder.Append(expression[i]);
            }

            ParseValue(result, valueBuilder);

            return result;
        }

        private static void ParseValue(List<ExpressionUnit> mathExpression, StringBuilder valueBuilder)
        {
            if (valueBuilder.Length == 0)
            {
                return;
            }

            var valueAsString = valueBuilder.ToString().Replace(',', '.');

            if (double.TryParse(valueAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                mathExpression.Add(new NumberExpressionUnit(valueAsString));

                valueBuilder.Clear();
            }
            else
            {
                throw new ArgumentException("Invalid mathematical expression or unsupported operation for parsing.");
            }
        }

        private static bool TryParseMathOperation(string expressionItem, MathOperation[] mathOperations, out ExpressionUnit expressionUnit)
        {
            var mathOperation = mathOperations.FirstOrDefault(m => m.ToString() == expressionItem);

            if (mathOperation != null)
            {
                expressionUnit = new OperationExpressionUnit(mathOperation.ToString());

                return true;
            }

            var leftBracketExpressionUnit = new LeftBracketExpressionUnit();

            if (expressionItem == leftBracketExpressionUnit.Value)
            {
                expressionUnit = leftBracketExpressionUnit;

                return true;
            }

            var rightBracketExpressionUnit = new RightBracketExpressionUnit();

            if (expressionItem == rightBracketExpressionUnit.Value)
            {
                expressionUnit = rightBracketExpressionUnit;

                return true;
            }

            expressionUnit = default(ExpressionUnit);

            return false;
        }
    }
}