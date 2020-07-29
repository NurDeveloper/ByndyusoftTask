using Calculator.Domain;

namespace Calculator.Interfaces
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
        IMathOperation GetOperationOrDefault(string keyword);

        /// <summary>
        /// Getting the keyword of an operation by characteristics
        /// </summary>
        /// <param name="operationCharacteristics">Characteristics of a mathematical operation</param>
        /// <returns>Returns the keyword of an operation or default instance</returns>
        string GetKeywordOrDefault(OperationCharacteristics operationCharacteristics);
    }
}