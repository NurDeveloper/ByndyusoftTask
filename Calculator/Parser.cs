using Calculator.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Calculator
{
    /// <summary>
    /// Parser of mathematical expression string
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Parse string to terms of mathematical expression
        /// </summary>
        /// <param name="expression">Input expression</param>
        /// <param name="mathOperations">Avaliable math operations</param>
        /// <returns></returns>
        public IEnumerable<object> Parse(string expression, MathOperation[] mathOperations)
        {
            var result = new List<object>();

            double value = 0;

            var valueBuilder = new StringBuilder();

            for (var i = 0; i < expression.Length; i++)
            {
                if (TryParseMathOperation(expression[i].ToString(), mathOperations, out MathOperation mathOperation))
                {
                    ParseValue(result, valueBuilder, out value);

                    result.Add(mathOperation);

                    continue;
                }

                valueBuilder.Append(expression[i]);
            }

            if (valueBuilder.Length != 0)
            {
                ParseValue(result, valueBuilder, out value);
            }

            return result;
        }

        private static void ParseValue(List<object> mathExpression, StringBuilder valueBuilder, out double value)
        {
            var valueAsString = valueBuilder.ToString();

            if (double.TryParse(valueAsString, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                mathExpression.Add(value);

                valueBuilder.Clear();
            }
        }

        private static bool TryParseMathOperation(string expressionItem, MathOperation[] mathOperations, out MathOperation mathOperation)
        {
            mathOperation = mathOperations.FirstOrDefault(m => m.ToString() == expressionItem);

            return mathOperation != null;
        }
    }
}