using Calculator.Domain.ExpressionUnits;
using Calculator.Interfaces;
using Calculator.MathOperations;
using Calculator.Tests.AssertExtensions;
using System;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class NotationConverterTests
    {
        private readonly IMathOperationsContainer _mathOperationsContainer;

        public NotationConverterTests()
        {
            var mathOperations = new List<IMathOperation>
            {
                new AddMathOperation(),
                new SubMathOperation(),
                new MulMathOperation(),
                new DivMathOperation(),
                new UnaryMinusMathOperation()
            };

            _mathOperationsContainer = new MathOperationsContainer(mathOperations);
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

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperationsContainer);

            var expectedItem = new NumberExpressionUnit("2.00");
            Assert.Collection(result, item => AssertExpressionUnit.Equal(expectedItem, item));
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

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("3.00"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("2.00"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("+"), item));
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

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("4.00"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("2.00"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("3.00"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("*"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("+"), item));
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

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("3.00"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("2.00"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("+"), item));
        }

        [Fact]
        public void NotationConverter_converts_digit_with_unary_minus_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<ExpressionUnit>()
            {
                new OperationExpressionUnit("-", "~"),
                new NumberExpressionUnit("8.00")
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("8.00"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-", "~"), item));
        }

        [Fact]
        public void NotationConverter_converts_digit_with_unary_minus_and_brackets_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<ExpressionUnit>()
            {
                new OperationExpressionUnit("-", "~"),
                new LeftBracketExpressionUnit(),
                new OperationExpressionUnit("-", "~"),
                new NumberExpressionUnit("8.00"),
                new RightBracketExpressionUnit()
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("8.00"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-", "~"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-", "~"), item));
        }

        [Fact]
        public void NotationConverter_converts_digit_with_unary_minus_and_sub_operation_in_brackets_to_RPN()
        {
            var notationConverter = new NotationConverter();
            var inputValue = new List<ExpressionUnit>()
            {
                new OperationExpressionUnit("-", "~"),
                new LeftBracketExpressionUnit(),
                new NumberExpressionUnit("8.00"),
                new OperationExpressionUnit("-"),
                new NumberExpressionUnit("2.00"),
                new RightBracketExpressionUnit()
            };

            var result = notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperationsContainer);

            Assert.Collection(result,
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("8.00"), item),
                item => AssertExpressionUnit.Equal(new NumberExpressionUnit("2.00"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-"), item),
                item => AssertExpressionUnit.Equal(new OperationExpressionUnit("-", "~"), item));
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

            Action action = () => notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperationsContainer);

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

            Action action = () => notationConverter.ConvertToReversePolishNotation(inputValue, _mathOperationsContainer);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Invalid mathematical expression or unsupported operation.", exception.Message);
        }
    }
}
