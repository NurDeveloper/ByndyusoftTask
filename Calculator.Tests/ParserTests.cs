using Calculator.Domain.ExpressionUnits;
using Calculator.Interfaces;
using Calculator.Tests.AssertExtensions;
using System;
using Xunit;

namespace Calculator.Tests
{
    public class ParserTests
    {
        private readonly IMathOperationsContainer _mathOperationsContainer = new MathOperationsContainer();

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

            var result = parser.Parse(inputValue, _mathOperationsContainer);

            var expectedItem = new NumberExpressionUnit("2");
            Assert.Collection(result, item => AssertExpressionUnit.Equal(expectedItem, item));
        }

        [Fact]
        public void Parser_parses_two_digit()
        {
            var parser = new Parser();
            string inputValue = "23";

            var result = parser.Parse(inputValue, _mathOperationsContainer);

            var expectedItem = new NumberExpressionUnit("23");
            Assert.Collection(result, item => AssertExpressionUnit.Equal(expectedItem, item));
        }

        [Fact]
        public void Parser_parses_digit_with_dot()
        {
            var parser = new Parser();
            string inputValue = "23.5";

            var result = parser.Parse(inputValue, _mathOperationsContainer);

            var expectedItem = new NumberExpressionUnit("23.5");
            Assert.Collection(result, item => AssertExpressionUnit.Equal(expectedItem, item));
        }

        [Fact]
        public void Parser_parses_digit_with_unary_minus()
        {
            var parser = new Parser();
            string inputValue = "-8";

            var result = parser.Parse(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-", "~"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("8"), item));
        }

        [Fact]
        public void Parser_parses_digit_with_unary_minus_and_brackets()
        {
            var parser = new Parser();
            string inputValue = "-(-8)";

            var result = parser.Parse(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-", "~"), item),
                item => AssertExpressionUnit.Equal(new LeftBracketExpressionUnit(), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-", "~"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("8"), item),
                item => AssertExpressionUnit.Equal(new RightBracketExpressionUnit(), item));
        }

        [Fact]
        public void Parser_parses_digit_with_unary_minus_and_sub_operation_in_brackets()
        {
            var parser = new Parser();
            string inputValue = "-(8-2)";

            var result = parser.Parse(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-", "~"), item),
                item => AssertExpressionUnit.Equal(new LeftBracketExpressionUnit(), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("8"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("2"), item),
                item => AssertExpressionUnit.Equal(new RightBracketExpressionUnit(), item));
        }

        [Fact]
        public void Parser_parses_digit_and_operation()
        {
            var parser = new Parser();
            string inputValue = "3+2";

            var result = parser.Parse(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("3"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("+"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("2"), item));
        }

        [Fact]
        public void Parser_parses_digit_and_operation_with_branches()
        {
            var parser = new Parser();
            string inputValue = "3.1*(2.7+5)";

            var result = parser.Parse(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("3.1"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("*"), item),
                item => AssertExpressionUnit.Equal(new LeftBracketExpressionUnit(), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("2.7"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("+"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("5"), item),
                item => AssertExpressionUnit.Equal(new RightBracketExpressionUnit(), item));
        }

        [Fact]
        public void Parser_throws_custom_exception_for_wrong_input_value()
        {
            var parser = new Parser();
            string inputValue = "5+b";

            Action action = () => parser.Parse(inputValue, _mathOperationsContainer);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Invalid mathematical expression or unsupported operation for parsing.", exception.Message);
        }

        [Fact]
        public void Parser_correct_parses_digit_with_comma()
        {
            var parser = new Parser();
            string inputValue = "23,5";

            var result = parser.Parse(inputValue, _mathOperationsContainer);

            Assert.Collection(result, item => AssertExpressionUnit.Equal(new NumberExpressionUnit("23.5"), item));
        }
    }
}
