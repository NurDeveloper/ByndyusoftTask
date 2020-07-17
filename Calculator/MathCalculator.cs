using Calculator.Interfaces;

namespace Calculator
{
    /// <summary>
    /// Mathematical calculator
    /// </summary>
    public class MathCalculator
    {
        private readonly MathOperation[] _operations;

        private readonly IParser _parser;
        private readonly INotationConverter _notationConverter;
        private readonly IMathProcessor _mathProcessor;

        /// <summary>
        /// Mathematical calculator constructor with dependency injection
        /// </summary>
        /// <param name="parser">Parser of mathematical expression string</param>
        /// <param name="notationConverter">Converter for different mathematical notation</param>
        /// <param name="mathProcessor">Processor for performing mathematical operations</param>
        /// <param name="operations">Mathematical operations</param>
        public MathCalculator(IParser parser, INotationConverter notationConverter, IMathProcessor mathProcessor, MathOperation[] operations)
        {
            _parser = parser;
            _notationConverter = notationConverter;
            _mathProcessor = mathProcessor;
            _operations = operations;
        }

        /// <summary>
        /// Calculate a mathematical expression
        /// </summary>
        /// <param name="expression">Mathematical expression</param>
        /// <returns>The result of the calculation</returns>
        public double Calculate(string expression)
        {
            var parsedExpression = _parser.Parse(expression, _operations);

            var convertedExpression = _notationConverter.ConvertToReversePolishNotation(parsedExpression);

            return _mathProcessor.Process(convertedExpression);
        }
    }
}
