using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using PinSharp.Api;
using Xunit;

namespace PinSharp.Tests
{
    public class PinterestApiMeTests
    {
        public IHttpClient HttpClient { get; set; }

        public string PinFieldsString { get; set; }
        public string UserPinFieldsString { get; set; }

        public PinterestApiMeTests()
        {
            HttpClient = Substitute.For<IHttpClient>();

            var pinFields = new[]
            {
                "id",
                "url",
                "link",
                "note",
                "attribution",
                "original_link",
                "color",
                "board",
                "counts",
                "created_at",
                "creator(id,url,first_name,last_name,username,image)",
                "image",
                "media",
                "metadata"
            };
            PinFieldsString = string.Join(",", pinFields);
            UserPinFieldsString = string.Join(",", pinFields.Where(x => !x.StartsWith("creator")));
        }

        [Fact]
        public async Task Test()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = new StringContent("{data:[],page: { cursor: null, next: null}}");
            HttpClient.GetAsync($"me/pins/?fields={UserPinFieldsString}").Returns(response);

            var client = new PinterestClient(HttpClient);
            var re = await client.Me.GetPinsAsync();

            re.Should();

            //HttpClient.Received().GetAsync(Arg.Is<string>(x => x.StartsWith("me/pins")));
        }

    }
}
