using Calculator.Domain.ExpressionUnits;
using Calculator.Interfaces;
using Calculator.MathOperations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class NotationConverterTests
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
        public void NotationConverter_should_be()
        {
            var notationConverter = new NotationConverter();
        }

        [Fact]
        public void NotationConverter_converts_one_digit_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<ExpressionUnit>
            {
                new NumberExpressionUnit("2.00")
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperations);

            var expectedItem = new NumberExpressionUnit("2.00");
            Assert.Collection(result, item => AssertExpressionUnitEqual(expectedItem, item));
        }

        [Fact]
        public void NotationConverter_converts_two_digit_and_operation_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("3.00"),
                new OperationExpressionUnit("+"),
                new NumberExpressionUnit("2.00")
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperations);

            Assert.Collection(result,
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("3.00"), item),
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("2.00"), item),
                item => AssertExpressionUnitEqual(new OperationExpressionUnit("+"), item));
        }

        [Fact]
        public void NotationConverter_converts_expression_with_first_priority_operation_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("4.00"),
                new OperationExpressionUnit("+"),
                new NumberExpressionUnit("2.00"),
                new OperationExpressionUnit("*"),
                new NumberExpressionUnit("3.00")
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperations);

            Assert.Collection(result,
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("4.00"), item),
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("2.00"), item),
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("3.00"), item),
                item => AssertExpressionUnitEqual(new OperationExpressionUnit("*"), item),
                item => AssertExpressionUnitEqual(new OperationExpressionUnit("+"), item));
        }

        [Fact]
        public void NotationConverter_converts_two_digit_and_operation_with_brackets_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<ExpressionUnit>()
            {
                new LeftBracketExpressionUnit(),
                new NumberExpressionUnit("3.00"),
                new OperationExpressionUnit("+"),
                new NumberExpressionUnit("2.00"),
                new RightBracketExpressionUnit()
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperations);

            Assert.Collection(result,
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("3.00"), item),
                item => AssertExpressionUnitEqual(new NumberExpressionUnit("2.00"), item),
                item => AssertExpressionUnitEqual(new OperationExpressionUnit("+"), item));
        }

        [Fact]
        public void NotationConverter_throws_custom_exception_for_wrong_brackets()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<ExpressionUnit>()
            {
                new RightBracketExpressionUnit(),
                new NumberExpressionUnit("3.00"),
                new OperationExpressionUnit("+"),
                new NumberExpressionUnit("2.00")
             };

            Action action = () => notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperations);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Invalid mathematical expression or non-infix notation.", exception.Message);
        }

        [Fact]
        public void NotationConverter_throws_custom_exception_for_unsupported_operation()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("3.00"),
                new OperationExpressionUnit("&"),
                new NumberExpressionUnit("2.00")
             };

            Action action = () => notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperations);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Invalid mathematical expression or unsupported operation.", exception.Message);
        }
    }
}
