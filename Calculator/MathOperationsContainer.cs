using Calculator.Domain.MathOperations;
using System.Collections.Generic;

namespace Calculator
{
    /// <summary>
    /// Container for mathematical operations. It has the characteristics
    /// of a Flyweight pattern
    /// </summary>
    public class MathOperationsContainer
    {
        private readonly Dictionary<string, MathOperation> _mathOperations;

        public MathOperationsContainer()
        {
            _mathOperations = new Dictionary<string, MathOperation>
            {
                { "+", new AddMathOperation() },
                { "-", new SubMathOperation() },
                { "*", new MulMathOperation() },
                { "/", new DivMathOperation() }
            };
        }

        /// <summary>
        /// Checking for the existence of an operation in a container
        /// </summary>
        /// <param name="keyword">Operation keyword</param>
        /// <returns>If it contains then returns true</returns>
        public bool ContainsOperation(string keyword)
        {
            return _mathOperations.ContainsKey(keyword);
        }

        /// <summary>
        /// Getting a link to an operation by keyword
        /// </summary>
        /// <param name="keyword">Operation keyword</param>
        /// <returns>Returns a link to the operation by keyword or default instance</returns>
        public MathOperation GetOperationOrDefault(string keyword)
        {
            _mathOperations.TryGetValue(keyword, out MathOperation mathOperation);

            return mathOperation;
        }
    }
}