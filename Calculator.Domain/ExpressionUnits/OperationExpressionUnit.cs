using Calculator.Domain.Enums;

namespace Calculator.Domain.ExpressionUnits
{
    /// <summary>
    /// Mathematical operation as part of an expression
    /// </summary>
    public class OperationExpressionUnit : ExpressionUnit
    {
        /// <summary>
        /// Mathematical operation constructor
        /// </summary>
        /// <param name="value">String representation of a mathematical operation</param>
        public OperationExpressionUnit(string value) : base(value, ExpressionUnitType.Operation)
        {
        }

        /// <summary>
        /// Mathematical operation constructor for different keyword and value
        /// </summary>
        /// <param name="value">String representation of a mathematical operation</param>
        /// <param name="keyword">Service keyword of the unit of expression</param>
        public OperationExpressionUnit(string value, string keyword) : base(value, keyword, ExpressionUnitType.Operation)
        {
        }
    }
}
