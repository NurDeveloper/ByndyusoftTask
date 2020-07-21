namespace Calculator.Domain.Enums
{
    /// <summary>
    /// Available types of expression unit
    /// </summary>
    public enum ExpressionUnitType
    {
        /// <summary>
        /// Real number
        /// </summary>
        Number,

        /// <summary>
        /// Mathematical operation
        /// </summary>
        Operation,

        /// <summary>
        /// Left bracket
        /// </summary>
        LeftBracket,

        /// <summary>
        /// Right bracket
        /// </summary>
        RightBracket
    }
}