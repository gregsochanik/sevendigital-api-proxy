using System.Net;
using NUnit.Framework;
using SevenDigital.Api.Schema;

namespace SevenDigital.Api.Proxy.Acceptance.Tests.Given_a_proxy_to_a_get_endpoint
{
	[TestFixture]
	public class When_I_make_an_invalid_xml_request
	{
		private RequestBuilder _requestBuilder;

		[SetUp]
		public void SetUp() {
			_requestBuilder = new RequestBuilder()
				.WithEndpoint("artist/details")
				.WithParameter("oauth_consumer_key", "YOUR_KEY_HERE");
		}

		[Test]
		public void Then_I_should_get_a_400() {
			HttpStatusCode httpStatusCode = _requestBuilder.GetStatusCode();
			Assert.That(httpStatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
		}

		[Test]
		public void Then_I_should_get_the_correct_response() {
			string response = _requestBuilder.GetResponseAsString();

			var expected = response.XmlDeserializeToType<Error>();

			Assert.That(expected.Code, Is.EqualTo(1001));
			Assert.That(expected.ErrorMessage, Is.EqualTo("Missing parameter artistId."));
		}
	}
}