using Calculator.Domain.Enums;

namespace Calculator.Domain.ExpressionUnits
{
    /// <summary>
    /// Expression unit as part of an expression
    /// </summary>
    public abstract class ExpressionUnit
    {
        /// <summary>
        /// Expression unit constructor
        /// </summary>
        /// <param name="value">String representation of the unit of expression</param>
        /// <param name="type">Type of expression unit</param>
        public ExpressionUnit(string value, ExpressionUnitType type)
        {
            Value = value;
            Type = type;
        }

        /// <summary>
        /// Type of expression unit
        /// </summary>
        public ExpressionUnitType Type { get; }

        /// <summary>
        /// String representation of the unit of expression
        /// </summary>
        public string Value { get; }
    }
}
