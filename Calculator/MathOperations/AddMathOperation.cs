namespace Calculator.MathOperations
{
    public class AddMathOperation : MathOperation
    {
        public override int Priority => 1;

        public override string ToString()
        {
            return "+";
        }
    }
}
