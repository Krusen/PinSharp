using FluentAssertions;
using PinSharp.Api;
using Xunit;

namespace PinSharp.Tests
{
    public class PathBuilderTests
    {
        [Fact]
        public void BuildPath_Ensures_Trailing_Forward_Slash_On_Path()
        {
            var pathWithoutTrailingSlash = PathBuilder.BuildPath("some/path", null);
            var pathWithTrailingSlash = PathBuilder.BuildPath("some/path/", null);

            pathWithoutTrailingSlash.Should().Be("some/path/");
            pathWithTrailingSlash.Should().Be("some/path/");
        }

        [Fact]
        public void BuildPath_Adds_Query_Parameter()
        {
            var query = "search query";
            var path = PathBuilder.BuildPath("some/path", new RequestOptions(query));

            path.Should().Be("some/path/?query=search query");
        }

        [Fact]
        public void BuildPath_Adds_Fields_Parameter()
        {
            var fields = new[] { "field1", "field2(f1,f2,f3)" };
            var path = PathBuilder.BuildPath("some/path", new RequestOptions(fields));

            path.Should().Be("some/path/?fields=field1,field2(f1,f2,f3)");
        }

        [Fact]
        public void BuildPath_Adds_Cursor_Parameter()
        {
            var cursor = "abcdefg";
            var path = PathBuilder.BuildPath("some/path", new RequestOptions {Cursor = cursor});

            path.Should().Be("some/path/?cursor=abcdefg");
        }

        [Fact]
        public void BuildPath_Adds_Limit_Parameter_Above_Zero()
        {
            var limit = 10;
            var path = PathBuilder.BuildPath("some/path", new RequestOptions {Limit = limit});

            path.Should().Be("some/path/?limit=10");
        }

        [Fact]
        public void BuildPath_Does_Not_Add_Limit_Parameter_Zero()
        {
            var limit = 0;
            var path = PathBuilder.BuildPath("some/path", new RequestOptions {Limit = limit});

            path.Should().Be("some/path/");
        }

        [Fact]
        public void BuildPath_Adds_Query_Fields_Cursor_And_Limit_Parameters()
        {
            var query = "search query";
            var fields = new[] {"field1", "field2(f1,f2,f3)"};
            var cursor = "abcdefg";
            var limit = 10;
            var path = PathBuilder.BuildPath("some/path", new RequestOptions(query, fields, cursor, limit));

            path.Should().Be("some/path/?query=search query&fields=field1,field2(f1,f2,f3)&cursor=abcdefg&limit=10");
        }
    }
}
