namespace Calculator.MathOperations
{
    public class MulMathOperation : MathOperation
    {
        public override int Priority => 2;

        public override string ToString()
        {
            return "*";
        }
    }
}
