using ConfigurationService.Domain.ValueObjects;
using Xunit;

namespace ConfigurationService.Domain.Tests.ValueObjectsTests
{
    public class OptionValueTests
    {
        [Theory]
        [InlineData("value")]
        [InlineData("")]
        [InlineData(null)]
        public void NewByString_Correct_Success(string val)
        {
            var v = new OptionValue(val);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void NewByBool_Correct_Success(bool val)
        {
            var v = new OptionValue(val);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NewByInt_Correct_Success(int val)
        {
            var v = new OptionValue(val);
        }

        [Fact]
        public void NewByStringArray_Empty_Success()
        {
            var v = new OptionValue(new string[]{});
        }

        [Fact]
        public void NewByStringArray_NotEmpty_Success()
        {
            var v = new OptionValue(new [] { "val" });
        }

        [Fact]
        public void NewByIntArray_Empty_Success()
        {
            var v = new OptionValue(new int[] { });
        }

        [Fact]
        public void NewByIntArray_NotEmpty_Success()
        {
            var v = new OptionValue(new[] { 1 });
        }
    }
}
