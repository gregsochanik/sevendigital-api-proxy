using OpenRasta.Configuration;
using OpenRasta.Web;

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
					.AsJsonDataContract().ForMediaType(MediaType.Json);
			}
		}
	}
}