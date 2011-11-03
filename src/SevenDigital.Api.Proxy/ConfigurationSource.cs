using OpenRasta.Configuration;
using OpenRasta.Web;
using SevenDigital.Api.Proxy.Codecs;

namespace SevenDigital.Api.Proxy
{
	public class ConfigurationSource : IConfigurationSource
	{
		public void Configure()
		{
			using (OpenRastaConfiguration.Manual)
			{
				ResourceSpace.Has
					.ResourcesOfType<object>()
					.AtUri("/")
					.HandledBy<ApiProxyHandler>()
					.AsXmlSerializer()
					.ForMediaType(new MediaType("text/xml")).ForMediaType(MediaType.Xml)
					.And
					.TranscodedBy<ServiceStackJsonCodec>()
					.ForMediaType(MediaType.Json);
			}
		}
	}
}