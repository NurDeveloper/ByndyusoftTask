using Calculator.Domain.Enums;

namespace Calculator.Domain.ExpressionUnits
{
    /// <summary>
    /// Right bracket as part of an expression
    /// </summary>
    public class RightBracketExpressionUnit : ExpressionUnit
    {
        /// <summary>
        /// Right bracket constructor
        /// </summary>
        public RightBracketExpressionUnit() : base(")", ExpressionUnitType.RightBracket)
        {
        }
    }
}
