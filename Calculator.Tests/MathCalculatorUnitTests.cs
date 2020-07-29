using Calculator.Domain.ExpressionUnits;
using Calculator.Interfaces;
using Calculator.MathOperations;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class MathCalculatorUnitTests
    {
        private readonly MathCalculator _calculator;

        private Mock<IParser> _mockParser;
        private Mock<INotationConverter> _mockNotationConverter;
        private Mock<IMathProcessor> _mockMathProcessor;

        private readonly IMathOperationsContainer _mathOperationsContainer;

        public MathCalculatorUnitTests()
        {
            IEnumerable<IMathOperation> mathOperations = new List<IMathOperation>
            {
                new AddMathOperation(),
                new SubMathOperation(),
                new MulMathOperation(),
                new DivMathOperation(),
                new UnaryMinusMathOperation()
            };

            _mathOperationsContainer = new MathOperationsContainer(mathOperations);

            _mockParser = new Mock<IParser>();
            _mockNotationConverter = new Mock<INotationConverter>();
            _mockMathProcessor = new Mock<IMathProcessor>();

            _calculator = new MathCalculator(_mockParser.Object, _mockNotationConverter.Object, _mockMathProcessor.Object, _mathOperationsContainer);
        }

        [Fact]
        public void Calculator_calculates()
        {
            const string inputExpression = "1+2*3";

            var parsedExpression = new List<ExpressionUnit>
            {
                new NumberExpressionUnit("1.00"),
                new OperationExpressionUnit("+"),
                new NumberExpressionUnit("2.00"),
                new OperationExpressionUnit("*"),
                new NumberExpressionUnit("3.00")
            };

            var convertedExpression = new List<ExpressionUnit>
            {
                new NumberExpressionUnit("1.00"),
                new NumberExpressionUnit("2.00"),
                new NumberExpressionUnit("3.00"),
                new OperationExpressionUnit("*"),
                new OperationExpressionUnit("+")
            };

            const double expectedResult = 7;

            _mockParser
                .Setup(p => p.Parse(inputExpression, _mathOperationsContainer))
                .Returns(parsedExpression);

            _mockNotationConverter
                .Setup(nc => nc.ConvertToReversePolishNotation(parsedExpression, _mathOperationsContainer))
                .Returns(convertedExpression);

            _mockMathProcessor
                .Setup(mp => mp.Process(convertedExpression, _mathOperationsContainer))
                .Returns(expectedResult);

            var actualResult = _calculator.Calculate(inputExpression);

            Assert.Equal("7", actualResult);
        }

        [Fact]
        public void Calculator_returns_error_message()
        {
            const string inputExpression = "3/0";

            var parsedExpression = new List<ExpressionUnit>
            {
                new NumberExpressionUnit("3.00"),
                new OperationExpressionUnit("/"),
                new NumberExpressionUnit("0.00"),
            };

            var convertedExpression = new List<ExpressionUnit>
            {
                new NumberExpressionUnit("3.00"),
                new NumberExpressionUnit("0.00"),
                new OperationExpressionUnit("/"),
            };

            const string expectedResult = "You cannot divide by zero.";

            _mockParser
                .Setup(p => p.Parse(inputExpression, _mathOperationsContainer))
                .Returns(parsedExpression);

            _mockNotationConverter
                .Setup(nc => nc.ConvertToReversePolishNotation(parsedExpression, _mathOperationsContainer))
                .Returns(convertedExpression);

            _mockMathProcessor
                .Setup(mp => mp.Process(convertedExpression, _mathOperationsContainer))
                .Throws(new DivideByZeroException(expectedResult));

            var actualResult = _calculator.Calculate(inputExpression);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
