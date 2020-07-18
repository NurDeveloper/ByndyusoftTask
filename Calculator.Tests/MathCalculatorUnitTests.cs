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

        private readonly MathOperation[] _mathOperations;

        public MathCalculatorUnitTests()
        {
            _mathOperations = new MathOperation[]
            {
                new AddMathOperation(),
                new SubMathOperation(),
                new MulMathOperation(),
                new DivMathOperation(),
                new LeftBracketMathOperation(),
                new RightBracketMathOperation()
            };

            _mockParser = new Mock<IParser>();
            _mockNotationConverter = new Mock<INotationConverter>();
            _mockMathProcessor = new Mock<IMathProcessor>();

            _calculator = new MathCalculator(_mockParser.Object, _mockNotationConverter.Object, _mockMathProcessor.Object, _mathOperations);
        }

        [Fact]
        public void Calculator_calculates()
        {
            const string inputExpression = "1+2*3";

            var parsedExpression = new List<object>
            {
                1.00,
                new AddMathOperation(),
                2.00,
                new MulMathOperation(),
                3.00
            };

            var convertedExpression = new List<object>
            {
                1.00,
                2.00,
                3.00,
                new MulMathOperation(),
                new AddMathOperation()
            };

            const double expectedResult = 7;

            _mockParser
                .Setup(p => p.Parse(inputExpression, _mathOperations))
                .Returns(parsedExpression);

            _mockNotationConverter
                .Setup(nc => nc.ConvertToReversePolishNotation(parsedExpression))
                .Returns(convertedExpression);

            _mockMathProcessor
                .Setup(mp => mp.Process(convertedExpression))
                .Returns(expectedResult);

            var actualResult = _calculator.Calculate(inputExpression);

            Assert.Equal("7", actualResult);
        }

        [Fact]
        public void Calculator_returns_error_message()
        {
            const string inputExpression = "3/0";

            var parsedExpression = new List<object>
            {
                3.00,
                new DivMathOperation(),
                0.00
            };

            var convertedExpression = new List<object>
            {
                3.00,
                0.00,
                new DivMathOperation()
            };

            const string expectedResult = "You cannot divide by zero.";

            _mockParser
                .Setup(p => p.Parse(inputExpression, _mathOperations))
                .Returns(parsedExpression);

            _mockNotationConverter
                .Setup(nc => nc.ConvertToReversePolishNotation(parsedExpression))
                .Returns(convertedExpression);

            _mockMathProcessor
                .Setup(mp => mp.Process(convertedExpression))
                .Throws(new DivideByZeroException(expectedResult));

            var actualResult = _calculator.Calculate(inputExpression);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
