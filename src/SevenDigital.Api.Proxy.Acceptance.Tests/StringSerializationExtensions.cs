using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.ArtistEndpoint;

namespace SevenDigital.Api.Proxy.Acceptance.Tests
{
	public static class StringSerializationExtensions
	{
		public static T XmlDeserializeToType<T>(this string response) {
			T toType;
			using (var stringReader = new StringReader(response)) {
				// Attempt to serialize it back to an artist
				var xmlSerializer = new XmlSerializer(typeof(Artist));
				object deserialize = xmlSerializer.Deserialize(stringReader);
				toType = (T)deserialize;
			}
			return toType;
		}

		public static T JsonDeserializeToType<T>(this string response) {
			T type;
			var memoryStream = new MemoryStream();
			using(var sr = new StreamWriter(memoryStream)) {
				sr.Write(response);
				sr.Flush();
				memoryStream.Position = 0;
				// Attempt to serialize it back to an artist
				var xmlSerializer = new DataContractJsonSerializer(typeof (Artist));
				object deserialize = xmlSerializer.ReadObject(memoryStream);
				type = (T) deserialize;
			}
			memoryStream.Close();
			return type;
		}
	}
}