using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace RunningJournalApi.UnitTests
{
    public class SimpleWebTokenTests
    {
        [Fact]
        public void SutIsIteratorOfClaims()
        {
            var sut = new SimpleWebToken();
            Assert.IsAssignableFrom<IEnumerable<Claim>>(sut);
        }

        [Fact]
        public void SutYieldsInjectedClaims()
        {
            var expected = new[]
            {
                new Claim("foo", "bar"),
                new Claim("baz", "qux"),
                new Claim("quux", "corge")
            };

            var sut = new SimpleWebToken(expected);
            Assert.True(expected.SequenceEqual(sut));
            Assert.True(
                expected.Cast<object>().SequenceEqual(
                    ((IEnumerable)sut).OfType<object>()));

        }

        [Theory]
        [InlineData(new string[0], "")]
        [InlineData(new[] { "foo|bar" }, "foo=bar")]
        [InlineData(new[] { "foo|bar", "baz|qux" }, "foo=bar&baz=qux")]
        public void ToStringReturnsCorrectResult(string[] keysAndValues, string expected)
        {
            var claims = keysAndValues
                .Select(s => s.Split('|'))
                .Select(s => new Claim(s[0], s[1]))
                .ToArray();

            var sut = new SimpleWebToken(claims);
            var actual = sut.ToString();
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData( "foo" )]
        [InlineData("invalid")]
        [InlineData(null)]
        public void TryParseInvalidStringReturnsFalse(string invalidString)
        {
            SimpleWebToken dummy;
            bool actual = SimpleWebToken.TryParse(invalidString, out dummy);
            Assert.False(actual);
        }

        [Theory]
        [InlineData(new object[] { new string[0]})]
        [InlineData(new object[] { new[] { "foo|bar" }})]
        [InlineData(new object[] { new[] { "foo|bar" ,"baz|qux" } })]
        public void TryParsevalidStringReturnsCorrectResult(string[] keysAndValues)
        {
            var expected = keysAndValues
               .Select(s => s.Split('|'))
               .Select(s => new Claim(s[0], s[1]))
               .ToArray();

            var tokenString = new SimpleWebToken(expected).ToString();
            SimpleWebToken actual;
            var isValid = SimpleWebToken.TryParse(tokenString, out actual);
            Assert.True(isValid, "Token string was not valid");

            Assert.True(expected.SequenceEqual(actual, new ClaimComparer()));
        }
    }
}
