using Calculator;
using Calculator.Interfaces;
using Calculator.MathOperations;
using System;
using System.Collections.Generic;

namespace CalculatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var mathOperations = new List<IMathOperation>
            {
                new AddMathOperation(),
                new SubMathOperation(),
                new MulMathOperation(),
                new DivMathOperation(),
                new UnaryMinusMathOperation()
            };

            var mathOperationsContainer = new MathOperationsContainer(mathOperations);
            var parser = new Parser();
            var notationConverter = new NotationConverter();
            var mathProcessor = new MathProcessor();

            var calculator = new MathCalculator(parser, notationConverter, mathProcessor, mathOperationsContainer);

            Console.WriteLine("Calculator");
            Console.WriteLine("To complete the work, type exit.");
            while (true)
            {
                Console.Write("Enter a math expression: ");
                var expression = Console.ReadLine();

                if (expression == "exit")
                {
                    Console.WriteLine("Work completed.");
                    break;
                }

                var result = calculator.Calculate(expression);
                Console.WriteLine("Result: {0}", result);

            }
        }
    }
}
