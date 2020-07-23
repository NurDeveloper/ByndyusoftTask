namespace Calculator.Domain.MathOperations
{
    /// <summary>
    /// Mathematical operation abstract class
    /// </summary>
    public abstract class MathOperation
    {
        public abstract int Priority { get; }
    }
}
