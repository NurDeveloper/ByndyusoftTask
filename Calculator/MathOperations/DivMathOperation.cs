namespace Calculator.MathOperations
{
    public class DivMathOperation : MathOperation
    {
        public override int Priority => 2;

        public override string ToString()
        {
            return "/";
        }
    }
}
