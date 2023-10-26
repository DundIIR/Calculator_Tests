﻿namespace Calculator_Parser
{
    public class Parser
    {
        private string InputString;
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

        public string StartParsing(string? SourceString)
        {
            InputString = SourceString ?? "0";
            /*if (string.IsNullOrWhiteSpace(InputString))
                throw new ArgumentNullException("String is null or contains whitespace");*/
            if(InputString == "0")
                return InputString;
            index = 0;
            GetSymbol();
            string result = MethodE().ToString("F8").TrimEnd('0').TrimEnd(',');
            if (isInvalid)
                return "Error";
            else if (result == double.NaN.ToString())
                return "Infinity";
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
                if (symbol == '\0')
                {
                    if (p == '+')
                        x += x;
                    else
                        x -= x;
                }
                else if (p == '+')
                    x += MethodT();
                else
                    x -= MethodT();
            }
            return x;
        }
        private double MethodT()
        {
            double tempx;
            double x = MethodM();
            while (symbol == '*' || symbol == '/')
            {
                char p = (char)symbol;
                GetSymbol();
                if(symbol == '\0')
                {
                    if (p == '*')
                        x *= x;
                    else
                        x = (x == 0) ? double.NaN : (x / x);
                }
                else if (p == '*')
                    x *= MethodM();
                else
                {
                    tempx = MethodM();
                    x = (tempx == 0) ? double.NaN : (x / tempx);
                }
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
                else if (char.IsLetter((char)symbol))
                {
                    /*throw new ArgumentException($"Invalid parameter. Current string:{InputString} ");*/
                    isInvalid = true;
                    return 0;
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
            if (x.LastOrDefault() == ',') x += '0';
            return double.Parse(x);
        }

        public string? StartParsing()
        {
            throw new NotImplementedException();
        }
    }
}