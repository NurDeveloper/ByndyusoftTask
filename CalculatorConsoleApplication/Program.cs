using Autofac;
using Calculator;
using Calculator.Interfaces;
using System;

namespace CalculatorConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = ConfigureContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var mathOperationsContainer = container.Resolve<IMathOperationsContainer>();
                var parser = scope.Resolve<IParser>();
                var notationConverter = scope.Resolve<INotationConverter>();
                var mathProcessor = scope.Resolve<IMathProcessor>();

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

        private static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MathOperationsContainer>().As<IMathOperationsContainer>().SingleInstance();
            builder.RegisterType<Parser>().As<IParser>().SingleInstance();
            builder.RegisterType<NotationConverter>().As<INotationConverter>().SingleInstance();
            builder.RegisterType<MathProcessor>().As<IMathProcessor>().SingleInstance();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(t => t.Name.EndsWith("MathOperation"))
                   .AsImplementedInterfaces();

            builder.RegisterType<MathProcessor>().As<IMathProcessor>().SingleInstance();

            var container = builder.Build();
            return container;
        }
    }
}
