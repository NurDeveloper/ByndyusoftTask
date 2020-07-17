using Calculator.MathOperations;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class NotationConverterTests
    {
        [Fact]
        public void NotationConverter_should_be()
        {
            var notationConverter = new NotationConverter();
        }

        [Fact]
        public void NotationConverter_converts_one_digit_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<object>
            {
                2.00
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue);

            Assert.Collection(result, item => Assert.Equal(2.00, item));
        }

        [Fact]
        public void NotationConverter_converts_two_digit_and_operation_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<object>()
            {
                3.00,
                new AddMathOperation(),
                2.00
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue);

            Assert.Collection(result,
                item => Assert.Equal(3.00, item),
                item => Assert.Equal(2.00, item),
                item => Assert.Equal("+", item.ToString()));
        }

        [Fact]
        public void NotationConverter_converts_expression_with_first_priority_operation_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<object>()
            {
                4.00,
                new AddMathOperation(),
                2.00,
                new MulMathOperation(),
                3.00
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue);

            Assert.Collection(result,
                item => Assert.Equal(4.00, item),
                item => Assert.Equal(2.00, item),
                item => Assert.Equal(3.00, item),
                item => Assert.Equal("*", item.ToString()),
                item => Assert.Equal("+", item.ToString()));
        }

        [Fact]
        public void NotationConverter_converts_two_digit_and_operation_with_brackets_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<object>()
            {
                new LeftBracketMathOperation(),
                3.00,
                new AddMathOperation(),
                2.00,
                new RightBracketMathOperation()
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue);

            Assert.Collection(result,
                item => Assert.Equal(3.00, item),
                item => Assert.Equal(2.00, item),
                item => Assert.Equal("+", item.ToString()));
        }
    }
}
