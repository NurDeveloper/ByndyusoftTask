using Calculator.Domain.ExpressionUnits;
using System.Collections.Generic;

namespace Calculator.Interfaces
{
    /// <summary>
    /// Processor for performing mathematical operations
    /// </summary>
    public interface IMathProcessor
    {
        /// <summary>
        /// Execute operations in a mathematical expression
        /// </summary>
        /// <param name="expression">Mathematical expression</param>
        /// <param name="mathOperations">Avaliable math operations</param>
        /// <returns>Execution result</returns>
        double Process(IEnumerable<ExpressionUnit> expression, MathOperation[] mathOperations);
    }
}