using Calculator.Domain.Enums;

namespace Calculator.Domain
{
    /// <summary>
    /// Characteristics of a mathematical operation
    /// </summary>
    public class OperationCharacteristics
    {
        /// <summary>
        /// Constructor for characteristics of a mathematical operation
        /// </summary>
        /// <param name="value">String representation of a mathematical operation</param>
        /// <param name="type">Type of operation</param>
        public OperationCharacteristics(string value, OperationType type)
        {
            Value = value;
            Type = type;
        }

        /// <summary>
        /// String representation of a mathematical operation.
        /// The candidate for the key word
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Type of operation
        /// </summary>
        public OperationType Type { get; }
    }
}
