using Calculator.Interfaces;

namespace Calculator.MathOperations
{
    public class LeftBracketMathOperation : MathOperation
    {
        public override int Priority => 0;

        public override string ToString()
        {
            return "(";
        }
    }
}
