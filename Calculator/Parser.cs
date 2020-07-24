using Calculator.Domain;
using Calculator.Domain.Enums;
using Calculator.Domain.ExpressionUnits;
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
                OperationType operationType = OperationType.Unary;

                var firstOperandExists = valueBuilder.Length != 0;
                if (firstOperandExists)
                {
                    operationType = OperationType.Binary;
                }

                if (TryParseExpressionUnit(expression[i].ToString(), operationType, mathOperationsContainer, out ExpressionUnit mathOperation))
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

        private static void ParseValue(IList<ExpressionUnit> mathExpression, StringBuilder valueBuilder)
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

        private static bool TryParseExpressionUnit(string expressionItem, OperationType operationType, IMathOperationsContainer mathOperationsContainer, out ExpressionUnit expressionUnit)
        {
            var keyword = mathOperationsContainer.GetKeywordOrDefault(new OperationCharacteristics(expressionItem, operationType));
            if (!string.IsNullOrEmpty(keyword))
            {
                expressionUnit = new OperationExpressionUnit(expressionItem, keyword);

                return true;
            }

            var leftBracketExpressionUnit = new LeftBracketExpressionUnit();

            if (expressionItem == leftBracketExpressionUnit.Keyword)
            {
                expressionUnit = leftBracketExpressionUnit;

                return true;
            }

            var rightBracketExpressionUnit = new RightBracketExpressionUnit();

            if (expressionItem == rightBracketExpressionUnit.Keyword)
            {
                expressionUnit = rightBracketExpressionUnit;

                return true;
            }

            expressionUnit = default(ExpressionUnit);

            return false;
        }
    }
}