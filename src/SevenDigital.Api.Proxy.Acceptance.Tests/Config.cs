using System.Configuration;

namespace SevenDigital.Api.Proxy.Acceptance.Tests
{
	public static class Config
	{
		public static string ApiBaseUrl { get; set; }

		static Config()
		{
			ApiBaseUrl = ConfigurationManager.AppSettings["Api.BaseUrl"];
		}
	}
}
