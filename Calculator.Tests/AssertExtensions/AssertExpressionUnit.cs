using Calculator.Domain.ExpressionUnits;
using Xunit;

namespace Calculator.Tests.AssertExtensions
{
    class AssertExpressionUnit
    {
        public static void Equal(ExpressionUnit expectedItem, ExpressionUnit actualItem)
        {
            Assert.Equal(expectedItem.Type, actualItem.Type);
            Assert.Equal(expectedItem.Keyword, actualItem.Keyword);
        }
    }
}
