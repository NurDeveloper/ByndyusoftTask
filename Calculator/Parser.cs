using Calculator.Domain;
using Calculator.Domain.Enums;
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

            var numberBuilder = new StringBuilder();

            for (var i = 0; i < expression.Length; i++)
            {
                OperationType operationType = OperationType.Unary;

                var firstOperandExists = numberBuilder.Length != 0;
                if (firstOperandExists)
                {
                    operationType = OperationType.Binary;
                }

                if (TryParseExpressionUnit(expression[i].ToString(), operationType, mathOperationsContainer, out ExpressionUnit mathOperation))
                {
                    ParseNumber(result, numberBuilder);

                    result.Add(mathOperation);

                    continue;
                }

                numberBuilder.Append(expression[i]);
            }

            ParseNumber(result, numberBuilder);

            return result;
        }

        private static void ParseNumber(IList<ExpressionUnit> mathExpression, StringBuilder numberBuilder)
        {
            if (numberBuilder.Length == 0)
            {
                return;
            }

            var numberAsString = numberBuilder.ToString().Replace(',', '.');

            if (double.TryParse(numberAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
            {
                mathExpression.Add(new NumberExpressionUnit(numberAsString));

                numberBuilder.Clear();
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