using Calculator.Domain.MathOperations;

namespace Calculator
{
    /// <summary>
    /// Container for mathematical operations
    /// </summary>
    public interface IMathOperationsContainer
    {
        /// <summary>
        /// Checking for the existence of an operation in a container
        /// </summary>
        /// <param name="keyword">Operation keyword</param>
        /// <returns>If it contains then returns true</returns>
        bool ContainsOperation(string keyword);

        /// <summary>
        /// Getting a reference to an operation by keyword
        /// </summary>
        /// <param name="keyword">Operation keyword</param>
        /// <returns>Returns a reference to the operation by keyword or default instance</returns>
        MathOperation GetOperationOrDefault(string keyword);
    }
}