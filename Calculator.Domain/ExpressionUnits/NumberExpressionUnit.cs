using Calculator.Domain.Enums;

namespace Calculator.Domain.ExpressionUnits
{
    /// <summary>
    /// Real number as part of an expression
    /// </summary>
    public class NumberExpressionUnit : ExpressionUnit
    {
        /// <summary>
        /// Real number constructor
        /// </summary>
        /// <param name="value">String representation of a real number</param>
        public NumberExpressionUnit(string value) : base(value, ExpressionUnitType.Number)
        {
        }

        /// <summary>
        /// Real number constructor for different culture settings
        /// </summary>
        /// <param name="value">String representation of a real number</param>
        /// <param name="keyword">Service keyword for culture-independent value</param>
        public NumberExpressionUnit(string value, string keyword) : base(value, keyword, ExpressionUnitType.Number)
        {
        }
    }
}
