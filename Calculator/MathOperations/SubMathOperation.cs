namespace Calculator.MathOperations
{
    public class SubMathOperation : MathOperation
    {
        public override int Priority => 1;

        public override string ToString()
        {
            return "-";
        }
    }
}
