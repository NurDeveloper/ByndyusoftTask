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
        /// <returns>Execution result</returns>
        double Process(IEnumerable<object> expression);
    }
}