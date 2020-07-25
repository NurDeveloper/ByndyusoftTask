﻿using Calculator.Domain.ExpressionUnits;
using Calculator.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class MathProcessorUnitTests
    {
        private readonly IMathOperationsContainer _mathOperationsContainer = new MathOperationsContainer();

        [Fact]
        public void MathProcessor_should_be()
        {
            var mathProcessor = new MathProcessor();
        }

        [Fact]
        public void MathProcessor_returns_one_number()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("2.00")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(2.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_add_operation()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("2.50"),
                new NumberExpressionUnit("3.25"),
                new OperationExpressionUnit("+")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(5.75, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_sub_operation()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("6.00"),
                new NumberExpressionUnit("2.00"),
                new OperationExpressionUnit("-")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(4.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_mul_operation()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("6.00"),
                new NumberExpressionUnit("2.00"),
                new OperationExpressionUnit("*")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(12.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_div_operation()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("6.00"),
                new NumberExpressionUnit("2.00"),
                new OperationExpressionUnit("/")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(3.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_several_math_operation()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("2.00"),
                new NumberExpressionUnit("3.00"),
                new NumberExpressionUnit("5.00"),
                new OperationExpressionUnit("*"),
                new OperationExpressionUnit("+")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(17.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_digit_with_comma()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("23,50", "23.50")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(23.50, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_digit_with_unary_minus()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("8.00"),
                new OperationExpressionUnit("-", "~")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(-8.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_digit_with_unary_minus_and_brackets()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("8.00"),
                new OperationExpressionUnit("-", "~"),
                new OperationExpressionUnit("-", "~")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(8.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_digit_with_unary_minus_and_sub_operation_in_brackets()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("8.00"),
                new NumberExpressionUnit("2.00"),
                new OperationExpressionUnit("-", "-"),
                new OperationExpressionUnit("-", "~")
            };

            var result = mathProcessor.Process(inputExpression, _mathOperationsContainer);

            Assert.Equal(-6.00, result);
        }

        [Fact]
        public void MathProcessor_returns_divide_by_zero_exception()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("3.00"),
                new NumberExpressionUnit("0.00"),
                new OperationExpressionUnit("/")
            };

            Action action = () => mathProcessor.Process(inputExpression, _mathOperationsContainer);

            var exception = Assert.Throws<DivideByZeroException>(action);
            Assert.Equal("You cannot divide by zero.", exception.Message);
        }

        [Fact]
        public void MathProcessor_should_throw_exception_for_operation_without_arg()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("3.00"),
                new NumberExpressionUnit("2.00"),
                new OperationExpressionUnit("*"),
                new OperationExpressionUnit("+")
            };

            Action action = () => mathProcessor.Process(inputExpression, _mathOperationsContainer);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Invalid mathematical expression.", exception.Message);
        }

        [Fact]
        public void MathProcessor_should_throw_custom_exception_for_unsupported_operation()
        {
            var mathProcessor = new MathProcessor();
            var inputExpression = new List<ExpressionUnit>()
            {
                new NumberExpressionUnit("3.00"),
                new NumberExpressionUnit("2.00"),
                new OperationExpressionUnit("$")
            };

            Action action = () => mathProcessor.Process(inputExpression, _mathOperationsContainer);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Invalid mathematical expression or unsupported operation.", exception.Message);
        }
    }
}
