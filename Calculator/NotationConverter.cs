using Calculator.Interfaces;
using Calculator.MathOperations;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    /// <summary>
    /// Converter for different mathematical notation
    /// </summary>
    public class NotationConverter
    {
        /// <summary>
        /// Convert infix notation to reverse polish notation
        /// </summary>
        /// <param name="infixNotation">Infix notation</param>
        /// <returns>Reverse polish notation</returns>
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
                    while (!(stack.Peek() is LeftBracketMathOperation))
                    {
                        result.Add(stack.Pop());
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
                result.Add(stack.Pop());
            }

            return result;
        }
    }
}
