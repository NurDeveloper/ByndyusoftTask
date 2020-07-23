using Calculator.Domain.ExpressionUnits;
using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        /// <param name="mathOperationsContainer">Avaliable math operations</param>
        /// <returns>Parsed mathematical expression</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public IEnumerable<ExpressionUnit> Parse(string expression, IMathOperationsContainer mathOperationsContainer)
        {
            var result = new List<ExpressionUnit>();

            var valueBuilder = new StringBuilder();

            for (var i = 0; i < expression.Length; i++)
            {
                if (TryParseMathOperation(expression[i].ToString(), mathOperationsContainer, out ExpressionUnit mathOperation))
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

        private static bool TryParseMathOperation(string expressionItem, IMathOperationsContainer mathOperationsContainer, out ExpressionUnit expressionUnit)
        {
            if (mathOperationsContainer.ContainsOperation(expressionItem))
            {
                expressionUnit = new OperationExpressionUnit(expressionItem);

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