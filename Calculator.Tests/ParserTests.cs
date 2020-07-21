using Calculator.Domain.ExpressionUnits;
using Calculator.Interfaces;
using Calculator.MathOperations;
using System;
using Xunit;

namespace Calculator.Tests
{
    public class ParserTests
    {
        private readonly MathOperation[] _mathOperations = new MathOperation[]
            {
                new AddMathOperation(),
                new SubMathOperation(),
                new MulMathOperation(),
                new DivMathOperation(),
                new LeftBracketMathOperation(),
                new RightBracketMathOperation()
            };

        [Fact]
        public void Parser_should_be()
        {
            var parser = new Parser();
        }

        [Fact]
        public void Parser_parses_one_digit()
        {
            var parser = new Parser();
            string inputValue = "2";

            var result = parser.Parse(inputValue, _mathOperations);

            Assert.Collection(result, item => Assert.Equal(2.00, item));
        }

        [Fact]
        public void Parser_parses_two_digit()
        {
            var parser = new Parser();
            string inputValue = "23";

            var result = parser.Parse(inputValue, _mathOperations);

            Assert.Collection(result, item => Assert.Equal(23.00, item));
        }

        [Fact]
        public void Parser_parses_digit_with_dot()
        {
            var parser = new Parser();
            string inputValue = "23.5";

            var result = parser.Parse(inputValue, _mathOperations);

            Assert.Collection(result, item => Assert.Equal(23.50, item));
        }

        [Fact]
        public void Parser_parses_digit_and_operation()
        {
            var parser = new Parser();
            string inputValue = "3+2";

            var result = parser.Parse(inputValue, _mathOperations);

            Assert.Collection(result,
                item => Assert.Equal(3.00, item),
                item => Assert.Equal("+", item.ToString()),
                item => Assert.Equal(2.00, item));
        }

        [Fact]
        public void Parser_parses_digit_and_operation_with_branches()
        {
            var parser = new Parser();
            string inputValue = "3.1*(2.7+5)";

            var result = parser.Parse(inputValue, _mathOperations);

            Assert.Collection(result,
                item => Assert.Equal(3.10, item),
                item => Assert.Equal("*", item.ToString()),
                item => Assert.Equal("(", item.ToString()),
                item => Assert.Equal(2.70, item),
                item => Assert.Equal("+", item.ToString()),
                item => Assert.Equal(5.00, item),
                item => Assert.Equal(")", item.ToString()));
        }

        [Fact]
        public void Parser_throws_custom_exception_for_wrong_input_value()
        {
            var parser = new Parser();
            string inputValue = "5+b";

            Action action = () => parser.Parse(inputValue, _mathOperations);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Invalid mathematical expression or unsupported operation for parsing.", exception.Message);
        }

        [Fact]
        public void Parser_correct_parses_digit_with_comma()
        {
            var parser = new Parser();
            string inputValue = "23,5";

            var result = parser.Parse(inputValue, _mathOperations);

            Assert.Collection(result, item => Assert.Equal(23.50, item));
        }
    }

    public class ParserSupportingCalculatorDomainTests
    {
        private readonly MathOperation[] _mathOperations = new MathOperation[]
            {
                new AddMathOperation(),
                new SubMathOperation(),
                new MulMathOperation(),
                new DivMathOperation(),
                new LeftBracketMathOperation(),
                new RightBracketMathOperation()
            };

        private static void AssertExpressionUnitEqual(ExpressionUnit expectedItem, ExpressionUnit item)
        {
            Assert.Equal(expectedItem.Type, item.Type);
            Assert.Equal(expectedItem.Value, item.Value);
        }

        [Fact]
        public void Parser_should_be()
        {
            var parser = new ParserSupportingCalculatorDomain();
        }

        [Fact]
        public void Parser_parses_one_digit()
        {
            var parser = new ParserSupportingCalculatorDomain();
            string inputValue = "2";

            var result = parser.Parse(inputValue, _mathOperations);

            var expectedItem = new NumberExpressionUnit("2");
            Assert.Collection(result, item => AssertExpressionUnitEqual(expectedItem, item));
        }

        [Fact]
        public void Parser_parses_two_digit()
        {
            var parser = new ParserSupportingCalculatorDomain();
            string inputValue = "23";

            var result = parser.Parse(inputValue, _mathOperations);

            var expectedItem = new NumberExpressionUnit("23");
            Assert.Collection(result, item => AssertExpressionUnitEqual(expectedItem, item));
        }

        [Fact]
        public void Parser_parses_digit_with_dot()
        {
            var parser = new ParserSupportingCalculatorDomain();
            string inputValue = "23.5";

            var result = parser.Parse(inputValue, _mathOperations);

            var expectedItem = new NumberExpressionUnit("23.5");
            Assert.Collection(result, item => AssertExpressionUnitEqual(expectedItem, item));
        }

        [Fact]
        public void Parser_parses_digit_and_operation()
        {
            var parser = new ParserSupportingCalculatorDomain();
            string inputValue = "3+2";

            var result = parser.Parse(inputValue, _mathOperations);

            Assert.Collection(result,
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("3"), item),
                item => AssertExpressionUnitEqual(new OperationExpressionUnit("+"), item),
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("2"), item));
        }

        [Fact]
        public void Parser_parses_digit_and_operation_with_branches()
        {
            var parser = new ParserSupportingCalculatorDomain();
            string inputValue = "3.1*(2.7+5)";

            var result = parser.Parse(inputValue, _mathOperations);

            Assert.Collection(result,
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("3.1"), item),
                item => AssertExpressionUnitEqual(new OperationExpressionUnit("*"), item),
                item => AssertExpressionUnitEqual(new LeftBracketExpressionUnit(), item),
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("2.7"), item),
                item => AssertExpressionUnitEqual(new OperationExpressionUnit("+"), item),
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("5"), item),
                item => AssertExpressionUnitEqual(new RightBracketExpressionUnit(), item));
        }

        [Fact]
        public void Parser_throws_custom_exception_for_wrong_input_value()
        {
            var parser = new ParserSupportingCalculatorDomain();
            string inputValue = "5+b";

            Action action = () => parser.Parse(inputValue, _mathOperations);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Invalid mathematical expression or unsupported operation for parsing.", exception.Message);
        }

        [Fact]
        public void Parser_correct_parses_digit_with_comma()
        {
            var parser = new ParserSupportingCalculatorDomain();
            string inputValue = "23,5";

            var result = parser.Parse(inputValue, _mathOperations);

            Assert.Collection(result, item => AssertExpressionUnitEqual(new NumberExpressionUnit("23.5"), item));
        }
    }
}
