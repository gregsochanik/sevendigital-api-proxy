using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace SevenDigital.Api.Proxy.Acceptance.Tests
{
	public static class StringSerializationExtensions
	{
		public static T XmlDeserializeToType<T>(this string response) {
			T outputType;
			using (var stringReader = new StringReader(response)) {
				var xmlSerializer = new XmlSerializer(typeof(T));
				object deserialize = xmlSerializer.Deserialize(stringReader);
				outputType = (T)deserialize;
			}
			return outputType;
		}

		public static T JsonDeserializeToType<T>(this string response) {
			T outputType;
			using(var memoryStream = new MemoryStream()) {
				using (var sr = new StreamWriter(memoryStream)) {
					sr.Write(response);
					sr.Flush();
					memoryStream.Position = 0;
					var xmlSerializer = new DataContractJsonSerializer(typeof (T));
					object deserialize = xmlSerializer.ReadObject(memoryStream);
					outputType = (T) deserialize;
				}
			}
			return outputType;
		}
	}
}