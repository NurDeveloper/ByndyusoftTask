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
    }
}
