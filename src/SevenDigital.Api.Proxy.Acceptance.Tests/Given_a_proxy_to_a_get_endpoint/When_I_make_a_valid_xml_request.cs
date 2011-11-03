using System.Net;
using NUnit.Framework;
using SevenDigital.Api.Schema.ArtistEndpoint;
using SevenDigital.Api.Testing;

namespace SevenDigital.Api.Proxy.Acceptance.Tests.Given_a_proxy_to_a_get_endpoint
{
	[TestFixture]
	public class When_I_make_a_valid_xml_request
	{
		private HttpRequestBuilder _httpRequestBuilder;

		[SetUp]
		public void SetUp()
		{
			_httpRequestBuilder = new HttpRequestBuilder()
								.WithEndpoint("artist/details")
								.WithParameter("oauth_consumer_key","YOUR_KEY_HERE")
								.WithParameter("artistId", "1");
		}

		[Test]
		public void Then_I_should_get_a_200()
		{
			HttpStatusCode httpStatusCode = _httpRequestBuilder.GetStatusCode();
			Assert.That(httpStatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Then_I_should_get_the_correct_response()
		{
			string response = _httpRequestBuilder.GetResponseAsString();

			var expectedArtist = response.XmlDeserializeToType<Artist>();

			Assert.That(expectedArtist.Id, Is.EqualTo(1));
			Assert.That(expectedArtist.Name, Is.EqualTo("Keane"));
			Assert.That(expectedArtist.SortName, Is.EqualTo("Keane"));
			Assert.That(expectedArtist.Image,
						Is.EqualTo("http://cdn.7static.com/static/img/artistimages/00/000/000/0000000001_150.jpg"));
			Assert.That(expectedArtist.Url,
						Is.EqualTo("http://www.7digital.com/artists/keane/?partner=1401"));
		}
	}
}