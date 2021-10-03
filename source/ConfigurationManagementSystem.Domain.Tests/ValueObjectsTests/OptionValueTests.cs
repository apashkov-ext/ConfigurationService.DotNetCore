using ConfigurationManagementSystem.Domain.ValueObjects;
using Xunit;

namespace ConfigurationManagementSystem.Domain.Tests.ValueObjectsTests
{
    public class OptionValueTests
    {
        [Theory]
        [InlineData("value")]
        [InlineData("")]
        [InlineData(null)]
        public void NewByString_Correct_Success(string val)
        {
            _ = new OptionValue(val);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void NewByBool_Correct_Success(bool val)
        {
            _ = new OptionValue(val);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void NewByInt_Correct_Success(int val)
        {
            _ = new OptionValue(val);
        }

        [Fact]
        public void NewByStringArray_Empty_Success()
        {
            _ = new OptionValue(new string[]{});
        }

        [Fact]
        public void NewByStringArray_NotEmpty_Success()
        {
            _ = new OptionValue(new [] { "val" });
        }

        [Fact]
        public void NewByIntArray_Empty_Success()
        {
            _ = new OptionValue(new int[] { });
        }

        [Fact]
        public void NewByIntArray_NotEmpty_Success()
        {
            _ = new OptionValue(new[] { 1 });
        }
    }
}
