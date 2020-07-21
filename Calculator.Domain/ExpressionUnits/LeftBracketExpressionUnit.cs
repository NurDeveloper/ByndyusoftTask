using Calculator.Domain.Enums;

namespace Calculator.Domain.ExpressionUnits
{
    /// <summary>
    /// Left bracket as part of an expression
    /// </summary>
    public class LeftBracketExpressionUnit : ExpressionUnit
    {
        /// <summary>
        /// Left bracket constructor
        /// </summary>
        public LeftBracketExpressionUnit() : base("(", ExpressionUnitType.LeftBracket)
        {
        }
    }
}
