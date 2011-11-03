using System;
using System.IO;
using System.Net;
using System.Xml;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace SevenDigital.Api.Proxy.PipelineContributors
{
	public class ApiRouter : IPipelineContributor
	{
		public void Initialize(IPipeline pipelineRunner)
		{
			pipelineRunner.Notify(RouteApiRequest).Before<KnownStages.IUriMatching>();
		}

		public PipelineContinuation RouteApiRequest(ICommunicationContext context)
		{
			// Get uri from pipelineData
			var passedApiUrl = (string)context.PipelineData["ApiUrl"];
			if (string.IsNullOrEmpty(passedApiUrl))
				return PipelineContinuation.Continue;
			
			// TODO : Push to config file
			var apiUrl = "http://api.7digital.com/1.2/" + passedApiUrl + context.Request.Uri.Query;

			string rawResponse = "";
			if(context.Request.HttpMethod == "GET")
			{
				var httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);

				WebResponse response = null;
				try
				{
					response = httpWebRequest.GetResponse();
				} 
				catch(WebException wex)
				{
					if(wex.Response != null)
						response = wex.Response;
				}

				if(response == null)
					throw new ArgumentException("response is null");

				using (var sr = new StreamReader(response.GetResponseStream()))
				{
					rawResponse = sr.ReadToEnd();
				}
			}

			// get xml response
			var xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(rawResponse);
			context.PipelineData.Add("ApiXmlResponse", xmlDocument);

			context.Request.Uri = new Uri(context.ApplicationBaseUri + "/");
			
			return PipelineContinuation.Continue;
		}
	}
}