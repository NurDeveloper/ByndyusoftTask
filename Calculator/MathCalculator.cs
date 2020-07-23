using Calculator.Interfaces;
using System;

namespace Calculator
{
    /// <summary>
    /// Mathematical calculator
    /// </summary>
    public class MathCalculator
    {
        private readonly IMathOperationsContainer _mathOperationsContainer;
        private readonly IParser _parser;
        private readonly INotationConverter _notationConverter;
        private readonly IMathProcessor _mathProcessor;

        /// <summary>
        /// Mathematical calculator constructor with dependency injection
        /// </summary>
        /// <param name="parser">Parser of mathematical expression string</param>
        /// <param name="notationConverter">Converter for different mathematical notation</param>
        /// <param name="mathProcessor">Processor for performing mathematical operations</param>
        /// <param name="mathOperationsContainer">Mathematical operations</param>
        public MathCalculator(IParser parser, INotationConverter notationConverter, IMathProcessor mathProcessor, IMathOperationsContainer mathOperationsContainer)
        {
            _parser = parser;
            _notationConverter = notationConverter;
            _mathProcessor = mathProcessor;
            _mathOperationsContainer = mathOperationsContainer;
        }

        /// <summary>
        /// Calculate a mathematical expression
        /// </summary>
        /// <param name="expression">Mathematical expression</param>
        /// <returns>The result of the calculation</returns>
        public string Calculate(string expression)
        {
            try
            {
                var parsedExpression = _parser.Parse(expression, _mathOperationsContainer);

                var convertedExpression = _notationConverter.ConvertToReversePolishNotation(parsedExpression, _mathOperationsContainer);

                var result = _mathProcessor.Process(convertedExpression, _mathOperationsContainer);

                return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
