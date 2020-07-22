using Calculator.Domain.ExpressionUnits;
using System.Collections.Generic;

namespace Calculator.Interfaces
{
    /// <summary>
    /// Converter for different mathematical notation
    /// </summary>
    public interface INotationConverter
    {
        /// <summary>
        /// Convert infix notation to reverse polish notation
        /// </summary>
        /// <param name="infixNotation">Infix notation</param>
        /// <param name="mathOperations">Avaliable math operations</param>
        /// <returns>Reverse polish notation</returns>
        IEnumerable<ExpressionUnit> ConvertToReversePolishNotation(IEnumerable<ExpressionUnit> infixNotation, MathOperation[] mathOperations);
    }
}