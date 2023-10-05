using Xunit;
using FluentAssertions;

namespace Calculator_Tests
{
    public class Tests
    {
        private Calculator _calculator;
        private Parser _parser;

        public Tests()
        {
            _calculator = new Calculator();
            _parser = new Parser();
        }

        // Calculation
        [Fact]
        public void Calculation_ShouldReturnNumberZero_IfSourceEmpty()
        {
            var result = _calculator.Calculation(string.Empty);
            result.Should().Be("0");
        }


        // Handler
        [Fact]
        public void Handler_ShouldReturnNotSpace_IfSource—ontainsSpaces()
        {
            var result = _calculator.Handler("   123 , 123 + ( 123 )  ");
            result.Should().Be("123,123+(123)");
        }

        [Fact]
        public void Handler_ShouldReturnNotDots_IfSource—ontainsDots()
        {
            var result = _calculator.Handler("123.123");
            result.Should().Be("123,123");
        }

        [Fact]
        public void Handler_ShouldReturnOneDotInNumber_IfSource—ontainsMultipleDotsInNumber()
        {
            var result = _calculator.Handler("123.123.456 + 123");
            result.Should().Be("123,123456+123");
        }


        [Theory]
        [InlineData("5+5", "10")]
        [InlineData("(5+5)*2", "20")]
        [InlineData("5+5*2", "15")]
        [InlineData("1", "1")]

        public void Parser_ShouldReturnResult_IfSourceContainsValidExpression(string input, string expected)
        {
            var result = _parser.StartParsing(input);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("2+(0.5)", "2,5")]
        [InlineData("2*2.6", "5,2")]
        public void Parser_ShouldReturnDoubleNumber_IfUserWritingDot(string left, string right)
        {
            var result = _parser.StartParsing(left);
            result.Should().Be(right);

        }

        [Fact]
        public void Parser_ShouldReturnZero_IfSourceContainsEmptyString()
        {
            _parser.Invoking(x => x.StartParsing("")).
            Should().Throw<System.ArgumentNullException>();
        }


    }
}

class Calculator
{
    public string Calculation(string input)
    {
        var result = input;
        if (string.IsNullOrWhiteSpace(result))
        {
            result = "0";
        }



        return result;
        throw new NotImplementedException();
    }

    public string Handler(string input)
    {
        var handlerInput = input;
        var result = "";
        var isDot = false;
        List<char> allowed—haracters = new List<char> { '-', '+', '*', '/', '(', ')' };
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
            else if (allowed—haracters.Contains(symbol))
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
