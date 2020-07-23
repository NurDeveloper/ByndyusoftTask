using Calculator;
using System;

namespace CalculatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var mathOperationsContainer = new MathOperationsContainer();
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
