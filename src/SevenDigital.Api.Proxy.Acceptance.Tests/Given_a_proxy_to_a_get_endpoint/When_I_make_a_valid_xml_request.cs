using System.Net;
using System.Xml;
using NUnit.Framework;

namespace SevenDigital.Api.Proxy.Acceptance.Tests.Given_a_proxy_to_a_get_endpoint
{
	[TestFixture]
	public class When_I_make_a_valid_xml_request
	{
		private RequestBuilder _requestBuilder;

		[SetUp]
		public void SetUp()
		{
			_requestBuilder = new RequestBuilder()
								.WithEndpoint("artist/details")
								.WithParameter("oauth_consumer_key","YOUR_KEY_HERE")
								.WithParameter("artistId", "1");
		}

		[Test]
		public void Then_I_should_get_a_200()
		{
			HttpStatusCode httpStatusCode = _requestBuilder.GetStatusCode();
			Assert.That(httpStatusCode, Is.EqualTo(HttpStatusCode.OK));
		}

		[Test]
		public void Then_I_should_get_the_correct_response()
		{
			XmlDocument responseAsXml = _requestBuilder.GetResponseAsXml();
			// Attempt to serialize it back to an artist
		}
	}
}