namespace Calculator_Tests
{
    public class Parser
    {
        private string? InputString;
        private bool isInvalid = false;
        private int symbol, index = 0;
        private void GetSymbol()
        {
            if (index < InputString.Length)
            {
                symbol = InputString[index];
                index++;
            }
            else
                symbol = '\0';
        }

        public string StartParsing(string SourceString)
        {
            InputString = SourceString;
            if (string.IsNullOrWhiteSpace(InputString))
                throw new ArgumentNullException("String is null or contains whitespace");
            index = 0;
            GetSymbol();
            string result = MethodE().ToString();
            if (isInvalid)
                return "Error";
            else
                return result;
        }
        private double MethodE()
        {
            double x = MethodT();
            while (symbol == '+' || symbol == '-')
            {
                char p = (char)symbol;
                GetSymbol();
                if (p == '+')
                    x += MethodT();
                else
                    x -= MethodT();
            }
            return x;
        }
        private double MethodT()
        {
            double x = MethodM();
            while (symbol == '*' || symbol == '/')
            {
                char p = (char)symbol;
                GetSymbol();
                if (p == '*')
                    x *= MethodM();
                else
                    x /= MethodM();
            }
            return x;
        }
        private double MethodM()
        {
            double x = 0;
            if (symbol == '(')
            {
                GetSymbol();
                x = MethodE();
                if (symbol != ')')
                {
                    isInvalid = true;
                    return 0;
                }
                GetSymbol();
                if (symbol >= '0' && symbol <= '9')
                {
                    isInvalid = true;
                    return 0;
                }
            }
            else
            {
                if (symbol == '-')
                {
                    GetSymbol();
                    x = -MethodM();
                }
                else if (symbol >= '0' && symbol <= '9')
                    x = MethodC();
                else if (symbol >= 'a' && symbol <= 'z')
                {
                    throw new ArgumentException($"Invalid parameter. Current string:{InputString} ");

                }
                else if (symbol == '(' || symbol == ')')
                {
                    isInvalid = true;
                    return 0;
                }

            }
            return x;
        }
        private double MethodC()
        {
            string x = "";
            bool isDot = false;
            double temp2 = 0;
            char temp = (char)symbol;
            while (symbol >= '0' && symbol <= '9')
            {

                x += (char)symbol;
                GetSymbol();
                if (symbol == '(')
                {
                    temp2 = MethodE();
                    if (symbol != ')')
                    {
                        isInvalid = true;
                        return 0;
                    }
                    GetSymbol();
                }
                if (symbol == '.' || symbol == ',')
                {
                    if (isDot)
                    {
                        isInvalid = true;
                        return 0;
                    }
                    x += ',';
                    GetSymbol();
                    isDot = true;
                }

            }
            return double.Parse(x);
        }
    }
}