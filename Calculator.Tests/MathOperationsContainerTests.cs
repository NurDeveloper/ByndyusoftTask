using Calculator.Domain;
using Calculator.Domain.Enums;
using Calculator.Interfaces;
using Calculator.MathOperations;
using System.Collections.Generic;
using Xunit;

namespace Calculator.Tests
{
    public class MathOperationsContainerTests
    {
        private readonly IMathOperationsContainer _mathOperationsContainer;

        public MathOperationsContainerTests()
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
        public void MathOperationsContainer_should_be()
        {
            IEnumerable<IMathOperation> mathOperations = new List<IMathOperation>
            {
                new AddMathOperation(),
                new SubMathOperation(),
                new MulMathOperation(),
                new DivMathOperation(),
                new UnaryMinusMathOperation()
            };

            var mathOperationsContainer = new MathOperationsContainer(mathOperations);
        }

        [Fact]
        public void MathOperationsContainer_returns_true_for_add_operation_by_keyword()
        {
            var keyword = "+";

            var result = _mathOperationsContainer.ContainsOperation(keyword);

            Assert.True(result);
        }

        [Fact]
        public void MathOperationsContainer_returns_true_for_sub_operation_by_keyword()
        {   
            var keyword = "-";

            var result = _mathOperationsContainer.ContainsOperation(keyword);

            Assert.True(result);
        }

        [Fact]
        public void MathOperationsContainer_returns_true_for_mul_operation_by_keyword()
        {
            var keyword = "*";

            var result = _mathOperationsContainer.ContainsOperation(keyword);

            Assert.True(result);
        }

        [Fact]
        public void MathOperationsContainer_returns_true_for_div_operation_by_keyword()
        {
            var keyword = "/";

            var result = _mathOperationsContainer.ContainsOperation(keyword);

            Assert.True(result);
        }

        [Fact]
        public void MathOperationsContainer_returns_true_for_unary_minus_operation_by_keyword()
        {
            var keyword = "~";

            var result = _mathOperationsContainer.ContainsOperation(keyword);

            Assert.True(result);
        }

        [Fact]
        public void MathOperationsContainer_returns_false_for_wrong_keyword()
        {
            var keyword = "&";

            var result = _mathOperationsContainer.ContainsOperation(keyword);

            Assert.False(result);
        }

        [Fact]
        public void MathOperationsContainer_returns_add_operation_by_keyword()
        {
            var keyword = "+";

            var stack = new Stack<double>();
            stack.Push(2.00);
            stack.Push(3.00);

            var mathOperation = _mathOperationsContainer.GetOperationOrDefault(keyword);

            mathOperation.Operate(stack);

            Assert.Equal(1, mathOperation.Priority);
            Assert.Equal(5.00, stack.Peek());
        }

        [Fact]
        public void MathOperationsContainer_returns_sub_operation_by_keyword()
        {
            var keyword = "-";

            var stack = new Stack<double>();
            stack.Push(6.00);
            stack.Push(4.00);

            var mathOperation = _mathOperationsContainer.GetOperationOrDefault(keyword);

            mathOperation.Operate(stack);

            Assert.Equal(1, mathOperation.Priority);
            Assert.Equal(2.00, stack.Peek());
        }

        [Fact]
        public void MathOperationsContainer_returns_mul_operation_by_keyword()
        {
            var keyword = "*";

            var stack = new Stack<double>();
            stack.Push(5.00);
            stack.Push(5.00);

            var mathOperation = _mathOperationsContainer.GetOperationOrDefault(keyword);

            mathOperation.Operate(stack);

            Assert.Equal(2, mathOperation.Priority);
            Assert.Equal(25.00, stack.Peek());
        }

        [Fact]
        public void MathOperationsContainer_returns_div_operation_by_keyword()
        {
            var keyword = "/";

            var stack = new Stack<double>();
            stack.Push(8.00);
            stack.Push(4.00);

            var mathOperation = _mathOperationsContainer.GetOperationOrDefault(keyword);

            mathOperation.Operate(stack);

            Assert.Equal(2, mathOperation.Priority);
            Assert.Equal(2.00, stack.Peek());
        }

        [Fact]
        public void MathOperationsContainer_returns_unary_minus_operation_by_keyword()
        {
            var keyword = "~";

            var stack = new Stack<double>();
            stack.Push(8.00);

            var mathOperation = _mathOperationsContainer.GetOperationOrDefault(keyword);

            mathOperation.Operate(stack);

            Assert.Equal(1, mathOperation.Priority);
            Assert.Equal(-8.00, stack.Peek());
        }

        [Fact]
        public void MathOperationsContainer_returns_default_operation_by_wrong_keyword()
        {
            var keyword = "&";

            var mathOperation = _mathOperationsContainer.GetOperationOrDefault(keyword);

            Assert.Null(mathOperation);
        }

        [Fact]
        public void MathOperationsContainer_returns_keyword_of_add_operation_by_characteristics()
        {
            var operationCharacteristics = new OperationCharacteristics("+", OperationType.Binary);

            var keyword = _mathOperationsContainer.GetKeywordOrDefault(operationCharacteristics);

            Assert.Equal("+", keyword);
        }

        [Fact]
        public void MathOperationsContainer_returns_keyword_of_sub_operation_by_characteristics()
        {
            var operationCharacteristics = new OperationCharacteristics("-", OperationType.Binary);

            var keyword = _mathOperationsContainer.GetKeywordOrDefault(operationCharacteristics);

            Assert.Equal("-", keyword);
        }

        [Fact]
        public void MathOperationsContainer_returns_keyword_of_mul_operation_by_characteristics()
        {
            var operationCharacteristics = new OperationCharacteristics("*", OperationType.Binary);

            var keyword = _mathOperationsContainer.GetKeywordOrDefault(operationCharacteristics);

            Assert.Equal("*", keyword);
        }

        [Fact]
        public void MathOperationsContainer_returns_keyword_of_div_operation_by_characteristics()
        {
            var operationCharacteristics = new OperationCharacteristics("/", OperationType.Binary);

            var keyword = _mathOperationsContainer.GetKeywordOrDefault(operationCharacteristics);

            Assert.Equal("/", keyword);
        }

        [Fact]
        public void MathOperationsContainer_returns_keyword_of_unary_minus_operation_by_characteristics()
        {
            var operationCharacteristics = new OperationCharacteristics("-", OperationType.Unary);

            var keyword = _mathOperationsContainer.GetKeywordOrDefault(operationCharacteristics);

            Assert.Equal("~", keyword);
        }

        [Fact]
        public void MathOperationsContainer_returns_default_keyword_for_unknown_value_and_binary()
        {
            var operationCharacteristics = new OperationCharacteristics("&", OperationType.Binary);

            var keyword = _mathOperationsContainer.GetKeywordOrDefault(operationCharacteristics);

            Assert.Null(keyword);
        }

        [Fact]
        public void MathOperationsContainer_returns_default_keyword_for_unknown_value_and_unary()
        {
            var operationCharacteristics = new OperationCharacteristics("&", OperationType.Unary);

            var keyword = _mathOperationsContainer.GetKeywordOrDefault(operationCharacteristics);

            Assert.Null(keyword);
        }
    }
}
