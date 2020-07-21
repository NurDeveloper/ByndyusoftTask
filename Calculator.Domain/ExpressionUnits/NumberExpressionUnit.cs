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
    }
}
