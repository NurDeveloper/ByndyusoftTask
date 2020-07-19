using Calculator.MathOperations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class MathProcessorUnitTests
    {
        [Fact]
        public void MathProcessor_should_be()
        {
            var mathProcessor = new MathProcessor();
        }

        [Fact]
        public void MathProcessor_returns_one_number()
        {
            var inputExpression = new List<object>()
            {
                2.00
            };

            var mathProcessor = new MathProcessor();
            var result = mathProcessor.Process(inputExpression);

            Assert.Equal(2.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_add_operation()
        {
            var inputExpression = new List<object>()
            {
                2.50,
                3.25,
                new AddMathOperation()
            };

            var mathProcessor = new MathProcessor();
            var result = mathProcessor.Process(inputExpression);

            Assert.Equal(5.75, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_sub_operation()
        {
            var inputExpression = new List<object>()
            {
                6.00,
                2.00,
                new SubMathOperation()
            };

            var mathProcessor = new MathProcessor();
            var result = mathProcessor.Process(inputExpression);

            Assert.Equal(4.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_mul_operation()
        {
            var inputExpression = new List<object>()
            {
                6.00,
                2.00,
                new MulMathOperation()
            };

            var mathProcessor = new MathProcessor();
            var result = mathProcessor.Process(inputExpression);

            Assert.Equal(12.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_div_operation()
        {
            var inputExpression = new List<object>()
            {
                6.00,
                2.00,
                new DivMathOperation()
            };

            var mathProcessor = new MathProcessor();
            var result = mathProcessor.Process(inputExpression);

            Assert.Equal(3.00, result);
        }

        [Fact]
        public void MathProcessor_returns_result_of_several_math_operation()
        { 
            var inputExpression = new List<object>()
            {
                2.00,
                3.00,
                5.00,
                new MulMathOperation(),
                new AddMathOperation()
            };

            var mathProcessor = new MathProcessor();
            var result = mathProcessor.Process(inputExpression);

            Assert.Equal(17.00, result);
        }

        [Fact]
        public void MathProcessor_returns_divide_by_zero_exception()
        {
            var inputExpression = new List<object>()
            {
                3.00,
                0.00,
                new DivMathOperation(),
            };

            var mathProcessor = new MathProcessor();
            Action action = () => mathProcessor.Process(inputExpression);

            var exception = Assert.Throws<DivideByZeroException>(action);
            Assert.Equal("You cannot divide by zero.", exception.Message);
        }

        [Fact]
        public void MathProcessor_should_throw_exception_for_operation_without_arg()
        {
            var inputExpression = new List<object>()
            {
                3.00,
                2.00,
                new MulMathOperation(),
                new AddMathOperation(),
            };

            var mathProcessor = new MathProcessor();
            Action action = () => mathProcessor.Process(inputExpression);

            var exception = Assert.Throws<ArgumentException>(action);
            Assert.Equal("Invalid mathematical expression.", exception.Message);
        }
    }
}
