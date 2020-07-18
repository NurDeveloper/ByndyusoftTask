using Calculator.Interfaces;
using Calculator.MathOperations;
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
        /// <returns>Reverse polish notation</returns>
        /// <exception cref="ArgumentException"></exception>
        public IEnumerable<object> ConvertToReversePolishNotation(IEnumerable<object> infixNotation)
        {
            var stack = new Stack<MathOperation>();
            var result = new List<object>();

            foreach (var item in infixNotation)
            {
                if (item is double)
                {
                    result.Add(item);

                    continue;
                }

                if (item is LeftBracketMathOperation leftBracketMathOperation)
                {
                    stack.Push(leftBracketMathOperation);

                    continue;
                }

                if (item is RightBracketMathOperation rightBracketMathOperation)
                {
                    while (stack.Count != 0 && !(stack.Peek() is LeftBracketMathOperation))
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

                var operation = item as MathOperation;

                while (stack.Count() != 0 && stack.Peek().Priority > operation.Priority)
                {
                    result.Add(stack.Pop());
                }

                stack.Push((MathOperation)item);
            }

            while (stack.Count() != 0)
            {
                var peek = stack.Peek();
                if ((peek is MathOperation) && !(stack.Peek() is LeftBracketMathOperation))
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
    }
}
