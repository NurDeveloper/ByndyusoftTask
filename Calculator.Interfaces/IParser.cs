using Calculator.Domain.ExpressionUnits;
using System.Collections.Generic;

namespace Calculator.Interfaces
{
    /// <summary>
    /// Parser of mathematical expression string
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Parse string to terms of mathematical expression
        /// </summary>
        /// <param name="expression">Input expression</param>
        /// <param name="mathOperations">Avaliable math operations</param>
        /// <returns>Parsed mathematical expression</returns>
        IEnumerable<ExpressionUnit> Parse(string expression, MathOperation[] mathOperations);
    }
}