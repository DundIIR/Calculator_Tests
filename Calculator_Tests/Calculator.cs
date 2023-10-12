using Calculator_Parser;

namespace EquationProcessing
{
    class Calculator
    {
        private Parser? _parser;
        public string Calculation(string input)
        {
            _parser = new Parser();
            var result = input;
            if (string.IsNullOrWhiteSpace(result))
            {
                result = "0";
            }

            result = _parser.StartParsing(Handler(result));


            return result;
            throw new NotImplementedException();
        }

        public string Handler(string input)
        {
            var handlerInput = input;
            var result = "";
            var isDot = false;
            List<char> allowedСharacters = new List<char> { '-', '+', '*', '/', '(', ')' };
            foreach (var symbol in handlerInput)
            {
                if (symbol == '.' || symbol == ',')
                {
                    if (!isDot)
                    {
                        isDot = true;
                        result += ',';
                    }
                }
                else if (char.IsDigit(symbol))
                {
                    result += symbol;
                }
                else if (allowedСharacters.Contains(symbol))
                {
                    isDot = false;
                    result += symbol;
                }
                else
                {
                    isDot = false;
                }
            }

            return result;
            throw new NotImplementedException();
        }
    }

}