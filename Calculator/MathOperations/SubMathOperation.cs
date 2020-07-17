using System.Collections.Generic;
using System.Linq;

namespace Calculator.MathOperations
{
    public class SubMathOperation : MathOperation, IOperation
    {
        public override int Priority => 1;

        public void Operate(Stack<double> stack)
        {
            if (stack != null && stack.Count() >= 2)
            {
                var result = stack.Pop();
                result = stack.Pop() - result;
                stack.Push(result);
            }
        }

        public override string ToString()
        {
            return "-";
        }
    }
}
