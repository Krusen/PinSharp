using FluentAssertions;
using PinSharp.Api.Exceptions;
using Xunit;

namespace PinSharp.Tests.Api.Exceptions
{
    public class PinSharpExceptionsTest
    {
        [Fact]
        public void Create_HttpStatusCode_ShouldBeSetToPassedValue()
        {
            var expectedStatusCode = 500;

            var exception = PinSharpException.Create<MockException>(null, null, null, expectedStatusCode);

            exception.HttpStatusCode.Should().Be(expectedStatusCode);
        }

        [Fact]
        public void Create_HttpStatusCode_NullValueShouldNotOverwriteExceptionValue()
        {
            var expectedStatusCode = MockException.DefaultStatusCode;

            var exception = PinSharpException.Create<MockException>(null, null, null, null);

            exception.HttpStatusCode.Should().Be(expectedStatusCode);
        }

        public class MockException : PinSharpException
        {
            public const int DefaultStatusCode = 400;

            public MockException(string message) : base(message)
            {
                HttpStatusCode = DefaultStatusCode;
            }
        }
    }
}
