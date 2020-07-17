namespace Calculator.MathOperations
{
    public class RightBracketMathOperation : MathOperation
    {
        public override int Priority => 0;

        public override string ToString()
        {
            return ")";
        }
    }
}
