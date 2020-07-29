using Calculator.Domain.Enums;
using Calculator.Domain.ExpressionUnits;
using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    /// <summary>
    /// Converter for different mathematical notation
    /// </summary>
    public class NotationConverter : INotationConverter
    {
        /// <summary>
        /// Convert infix notation to reverse polish notation
        /// </summary>
        /// <param name="infixNotation">Infix notation</param>
        /// <param name="mathOperationsContainer">Avaliable math operations</param>
        /// <returns>Reverse polish notation</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public IEnumerable<ExpressionUnit> ConvertToReversePolishNotation(IEnumerable<ExpressionUnit> infixNotation, IMathOperationsContainer mathOperationsContainer)
        {
            var stack = new Stack<ExpressionUnit>();
            var result = new List<ExpressionUnit>();

            foreach (var item in infixNotation)
            {
                if (item.Type == ExpressionUnitType.Number)
                {
                    result.Add(item);

                    continue;
                }

                if (item.Type == ExpressionUnitType.LeftBracket)
                {
                    stack.Push(item);

                    continue;
                }

                if (item.Type == ExpressionUnitType.RightBracket)
                {
                    while (stack.Count != 0 && !(stack.Peek().Type == ExpressionUnitType.LeftBracket))
                    {
                        result.Add(stack.Pop());
                    }

                    if (stack.Count == 0)
                    {
                        throw new ArgumentException("Invalid mathematical expression or non-infix notation.");
                    }

                    stack.Pop();

                    continue;
                }

                var operation = GetOperationByKeyword(item, mathOperationsContainer);

                while (stack.Count() != 0)
                {
                    var peek = stack.Peek();
                    if (peek.Type != ExpressionUnitType.Operation)
                    {
                        break;
                    }

                    if (GetOperationByKeyword(peek, mathOperationsContainer).Priority <= operation.Priority)
                    {
                        break;
                    }

                    result.Add(stack.Pop());
                }

                stack.Push(item);
            }

            while (stack.Count() != 0)
            {
                var peek = stack.Peek();
                if ((peek.Type == ExpressionUnitType.Operation) && !(peek.Type == ExpressionUnitType.LeftBracket))
                {
                    result.Add(stack.Pop());
                }
                else
                {
                    throw new ArgumentException("Invalid mathematical expression or non-infix notation.");
                }
            }

            return result;
        }

        private static IMathOperation GetOperationByKeyword(ExpressionUnit item, IMathOperationsContainer mathOperationsContainer)
        {
            var operation = mathOperationsContainer.GetOperationOrDefault(item.Keyword);

            if (operation == null)
            {
                throw new ArgumentException("Invalid mathematical expression or unsupported operation.");
            }

            return operation;
        }
    }
}
