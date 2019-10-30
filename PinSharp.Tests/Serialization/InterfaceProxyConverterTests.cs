using System;
using AutoFixture.Xunit2;
using FluentAssertions;
using Newtonsoft.Json;
using PinSharp.Models;
using PinSharp.Serialization;
using Xunit;

namespace PinSharp.Tests.Serialization
{
    public class InterfaceProxyConverterTests
    {
        private const string Json = @"{
            ""attribution"": null,
            ""creator"": {
                ""url"": ""https://www.pinterest.com/krusenen/"",
                ""first_name"": ""Søren"",
                ""last_name"": ""Kruse"",
                ""id"": ""332562891142082219""
            },
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
                ""comments"": 2
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
        }";

        [Theory]
        [AutoMockedData]
        public void InterfaceIsProxied([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            pin.GetType().Name.Should().Be("IPinProxy");
        }

        [Theory]
        [AutoMockedData]
        public void SimpleProperties([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            pin.Id.Should().Be("332562753713076744");
            pin.Note.Should().Be("BWM S1000RR");
            pin.Color.Should().Be("#837978");
            pin.Url.Should().Be("https://www.pinterest.com/pin/332562753713076744/");
            pin.Link.Should().Be("https://www.pinterest.com/r/pin/332562753713076744/4802051985327008577/3595a4fd5b6a535991832a2a3c4abaa4ecfb4d2bf65988bad65058e4b0c83739");
        }

        [Theory]
        [AutoMockedData]
        public void CustomPropertyNames([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            pin.OriginalLink.Should().Be("http://motorcycles-and-more.tumblr.com/post/127108486524/bwm-s1000rr");
            pin.CreatedAt.Should().Be(DateTime.Parse("2015-11-18T18:00:25"));
        }

        [Theory]
        [AutoMockedData]
        public void Creator([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            pin.Creator.Should().NotBeNull();
            pin.Creator.Url.Should().Be("https://www.pinterest.com/krusenen/");
            pin.Creator.FirstName.Should().Be("Søren");
            pin.Creator.LastName.Should().Be("Kruse");
            pin.Creator.Id.Should().Be("332562891142082219");
        }

        [Theory]
        [AutoMockedData]
        public void Media([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            // IDictionary<string, string>
            pin.Media.Should().NotBeEmpty();
            pin.Media["type"].Should().Be("image");
        }

        [Theory]
        [AutoMockedData]
        public void Board([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            pin.Board.Should().NotBeNull();
            pin.Board.Url.Should().Be("https://www.pinterest.com/krusenen/public-mc-test/");
            pin.Board.Id.Should().Be("332562822422998092");
            pin.Board.Name.Should().Be("Public MC Test");
        }

        [Theory]
        [AutoMockedData]
        public void Images([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            // Custom property name, object
            pin.Images.Original.Should().NotBeNull();
            pin.Images.Original.Url.Should().Be("https://i.pinimg.com/originals/eb/f9/fe/ebf9fe6b5eb30c13ecac7ca9e27240bc.jpg");
            pin.Images.Original.Width.Should().Be(640);
            pin.Images.Original.Height.Should().Be(640);
        }

        [Theory]
        [AutoMockedData]
        public void Counts([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            pin.Counts.Should().NotBeNull();
            pin.Counts.Saves.Should().Be(1);
            pin.Counts.Comments.Should().Be(2);
        }

        [Theory]
        [AutoMockedData]
        public void MetaData([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            // IDictionary<string, JObject>, object of objects
            pin.Metadata.Should().NotBeEmpty();
            pin.Metadata["link"]["locale"].ToObject<string>().Should().Be("es");
        }

        [Theory]
        [AutoMockedData]
        public void Attribution([NoAutoProperties] InterfaceProxyConverter converter)
        {
            var pin = JsonConvert.DeserializeObject<IPin>(Json, converter);

            pin.Attribution.Should().BeNull();
        }
    }
}
