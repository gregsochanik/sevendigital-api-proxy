using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;

namespace SevenDigital.Api.Proxy.Acceptance.Tests
{
	public class RequestBuilder
	{
		private string _endpoint;
		private string _accept;
		private string _queryString = "?";
		
		public RequestBuilder WithEndpoint(string endpoint)
		{
			_endpoint = endpoint;
			return this;
		}

		public RequestBuilder WithAccept(string accept)
		{
			_accept = accept;
			return this;
		}

		public RequestBuilder WithParameter(string key, string value)
		{
			_queryString += key + "=" + value + "&";
			return this;
		}

		public HttpStatusCode GetStatusCode()
		{
			var request = PrepareRequest();
			using(var webResponse = (HttpWebResponse)request.GetResponse())
			{
				return webResponse.StatusCode;
			}
		}

		public XmlDocument GetResponseAsXml()
		{
			var doc = new XmlDocument();
			doc.LoadXml(GetResponseAsString());
			return doc;
		}

		public string GetResponseAsString()
		{
			var request = PrepareRequest();
			using (var webResponse = (HttpWebResponse)request.GetResponse())
			{
				using (var sr = new StreamReader(webResponse.GetResponseStream()))
				{
					return sr.ReadToEnd();
				}
			}
		}

		private HttpWebRequest PrepareRequest()
		{
			string fullPath = Config.ApiBaseUrl + _endpoint + _queryString;
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(fullPath.TrimEnd('&'));
			if(!String.IsNullOrEmpty(_accept))
				httpWebRequest.Accept = _accept;
			return httpWebRequest;
		}
	}
}