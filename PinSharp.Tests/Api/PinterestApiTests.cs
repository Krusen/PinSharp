using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using PinSharp.Api;
using PinSharp.Http;
using Xunit;

namespace PinSharp.Tests.Api
{
    public class PinterestApiTests
    {
        #region Data
        private const string JsonData = @"{
    ""data"": [
        {
            ""attribution"": null,
            ""url"": ""https://www.pinterest.com/pin/332562753717927125/"",
            ""media"": {
                ""type"": ""image""
            },
            ""created_at"": ""2016-10-31T10:32:47"",
            ""original_link"": ""http://thewriteconversation.blogspot.com/2015/08/organize-your-life-as-writer.html"",
            ""note"": ""Organize Your Life as a Writer"",
            ""color"": ""#b59b65"",
            ""link"": ""https://www.pinterest.com/r/pin/332562753717927125/4802051985327008577/f799a73e240a2b3bdc9b45c5f887d1c9952af8059feb9d63c59e32af6479b2ba"",
            ""board"": {
                ""url"": ""https://www.pinterest.com/krusenen/business/"",
                ""id"": ""332562822423108543"",
                ""name"": ""Business""
            },
            ""image"": {
                ""original"": {
                    ""url"": ""https://i.pinimg.com/originals/55/5e/36/555e36ad52ce5491450c4865fd9121cc.jpg"",
                    ""width"": 400,
                    ""height"": 400
                }
            },
            ""counts"": {
                ""saves"": 0,
                ""comments"": 0
            },
            ""id"": ""332562753717927125"",
            ""metadata"": {}
        },
        {
            ""attribution"": null,
            ""url"": ""https://www.pinterest.com/pin/332562753717895561/"",
            ""media"": {
                ""type"": ""image""
            },
            ""created_at"": ""2016-10-29T11:11:16"",
            ""original_link"": """",
            ""note"": ""test note 2000"",
            ""color"": ""#717a77"",
            ""link"": """",
            ""board"": {
                ""url"": ""https://www.pinterest.com/krusenen/public-mc-test/"",
                ""id"": ""332562822422998092"",
                ""name"": ""Public MC Test""
            },
            ""image"": {
                ""original"": {
                    ""url"": ""https://i.pinimg.com/originals/e9/07/4f/e9074fdf4f1fdcb8b99fb85cb7d9d310.jpg"",
                    ""width"": 750,
                    ""height"": 701
                }
            },
            ""counts"": {
                ""saves"": 1,
                ""comments"": 0
            },
            ""id"": ""332562753717895561"",
            ""metadata"": {}
        },
        {
            ""attribution"": null,
            ""url"": ""https://www.pinterest.com/pin/332562753713418112/"",
            ""media"": {
                ""type"": ""image""
            },
            ""created_at"": ""2015-12-18T21:54:55"",
            ""original_link"": ""https://www.sunfrog.com/Drinking/WINE-64157169-Guys.html"",
            ""note"": ""qwerqwer"",
            ""color"": ""#c3c3c4"",
            ""link"": ""https://www.pinterest.com/r/pin/332562753713418112/4802051985327008577/2cf3ad75cd11555820cd43b939eb44095eedece1d14d28db9c3e387e9c2714f2"",
            ""board"": {
                ""url"": ""https://www.pinterest.com/krusenen/public-mc-test/"",
                ""id"": ""332562822422998092"",
                ""name"": ""Public MC Test""
            },
            ""image"": {
                ""original"": {
                    ""url"": ""https://i.pinimg.com/originals/54/3b/99/543b998fa99ea3ffa5a5e2e6db715d5f.jpg"",
                    ""width"": 1010,
                    ""height"": 1010
                }
            },
            ""counts"": {
                ""saves"": 1,
                ""comments"": 0
            },
            ""id"": ""332562753713418112"",
            ""metadata"": {}
        },
        {
            ""attribution"": null,
            ""url"": ""https://www.pinterest.com/pin/332562753713076744/"",
            ""media"": {
                ""type"": ""image""
            },
            ""created_at"": ""2015-11-18T18:00:25"",
            ""original_link"": ""http://motorcycles-and-more.tumblr.com/post/127108486524/bwm-s1000rr"",
            ""note"": ""BWM S1000RR"",
            ""color"": ""#837978"",
            ""link"": ""https://www.pinterest.com/r/pin/332562753713076744/4802051985327008577/3595a4fd5b6a535991832a2a3c4abaa4ecfb4d2bf65988bad65058e4b0c83739"",
            ""board"": {
                ""url"": ""https://www.pinterest.com/krusenen/public-mc-test/"",
                ""id"": ""332562822422998092"",
                ""name"": ""Public MC Test""
            },
            ""image"": {
                ""original"": {
                    ""url"": ""https://i.pinimg.com/originals/eb/f9/fe/ebf9fe6b5eb30c13ecac7ca9e27240bc.jpg"",
                    ""width"": 640,
                    ""height"": 640
                }
            },
            ""counts"": {
                ""saves"": 1,
                ""comments"": 0
            },
            ""id"": ""332562753713076744"",
            ""metadata"": {
                ""link"": {
                    ""locale"": ""es"",
                    ""title"": ""Motorcycles, bikers and more"",
                    ""site_name"": ""Motorcycles, bikers and more"",
                    ""description"": ""BWM S1000RR"",
                    ""favicon"": ""https://i.pinimg.com/favicons/958382540edf2e694e19da0388c9209c063be8c3d994f7788dc14cf0.pnj?ea0a6c0f80a57150161ff6bf2a369cab""
                }
            }
        },
        {
            ""attribution"": null,
            ""url"": ""https://www.pinterest.com/pin/332562753713076738/"",
            ""media"": {
                ""type"": ""image""
            },
            ""created_at"": ""2015-11-18T17:59:59"",
            ""original_link"": """",
            ""note"": ""BWM S1000RR #gripstertankgrips www.techspec-usa.com"",
            ""color"": ""#b2a39d"",
            ""link"": """",
            ""board"": {
                ""url"": ""https://www.pinterest.com/krusenen/public-mc-test/"",
                ""id"": ""332562822422998092"",
                ""name"": ""Public MC Test""
            },
            ""image"": {
                ""original"": {
                    ""url"": ""https://i.pinimg.com/originals/0d/c1/be/0dc1be7cb9e830d323fc7cc844a3abc8.jpg"",
                    ""width"": 800,
                    ""height"": 600
                }
            },
            ""counts"": {
                ""saves"": 0,
                ""comments"": 0
            },
            ""id"": ""332562753713076738"",
            ""metadata"": {}
        }
    ],
    ""page"": {
        ""cursor"": null,
        ""next"": null
    }
}";
        #endregion

        private const string RateLimitHeader = "X-Ratelimit-Limit";
        private const string RateLimitRemainingHeader = "X-Ratelimit-Remaining";

        [Theory]
        [AutoMockedData]
        public async Task Asdf(IHttpClient httpClient)
        {
            // Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add(RateLimitHeader, "1");
            response.Headers.Add(RateLimitRemainingHeader, "1");
            response.Content = new StringContent(JsonData);
            //response.Content.ReadAsStringAsync().Returns(JsonData);
            httpClient.GetAsync("").ReturnsForAnyArgs(response);

            // Act
            var api = new PinterestApi(httpClient);
            var pins = await api.GetPinsAsync("user/board");

            // Assert
            pins.Should().NotBeNull();
            pins.First().Board.Id.Should().Be("332562822423108543");
            pins.First().Board.Name.Should().Be("Business");
            pins.First().Board.Url.Should().Be("https://www.pinterest.com/krusenen/business/");
        }

    }
}
