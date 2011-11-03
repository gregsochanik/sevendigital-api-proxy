﻿using System;
using System.IO;
using System.Net;
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
			try {
				using (var webResponse = (HttpWebResponse) request.GetResponse()) {
					return webResponse.StatusCode;
				}
			} catch (WebException wex) {
				if(wex.Response != null) {
					return ((HttpWebResponse) wex.Response).StatusCode;
				}
				throw;
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
			try {
				using (var webResponse = (HttpWebResponse) request.GetResponse()) {
					using (var sr = new StreamReader(webResponse.GetResponseStream())) {
						return sr.ReadToEnd();
					}
				}
			} catch (WebException wex) {
				if (wex.Response != null) {
					using (var sr = new StreamReader(wex.Response.GetResponseStream())) {
						return sr.ReadToEnd();
					}
				}
				throw;
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