using System.IO;
using OpenRasta.Codecs;
using OpenRasta.Web;
using ServiceStack.Text;

namespace SevenDigital.Api.Proxy.Codecs {
	public class ServiceStackJsonCodec : IMediaTypeWriter 
	{
		public object Configuration { get; set; }

		public void WriteTo(object entity, IHttpEntity response, string[] codecParameters) {
			using (var streamWriter = new StreamWriter(response.Stream)) {
				streamWriter.Write(JsonSerializer.SerializeToString(entity));
			}
		}
	}
}
