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
            Keyword = value;
            Type = type;
        }

        /// <summary>
        /// Expression unit constructor for different keyword and value
        /// </summary>
        /// <param name="value">String representation of the unit of expression</param>
        /// <param name="keyword">Service keyword of the unit of expression</param>
        /// <param name="type">Type of expression unit</param>
        public ExpressionUnit(string value, string keyword, ExpressionUnitType type)
        {
            Value = value;
            Keyword = keyword;
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

        /// <summary>
        /// Service keyword of the unit of expression
        /// </summary>
        public string Keyword { get; }
    }
}
