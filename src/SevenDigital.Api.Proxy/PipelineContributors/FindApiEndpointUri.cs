using System;
using System.Linq;
using OpenRasta.Pipeline;
using OpenRasta.Web;

namespace SevenDigital.Api.Proxy.PipelineContributors
{
	public class FindApiEndpointUri : IPipelineContributor
	{
		public void Initialize(IPipeline pipelineRunner)
		{
			pipelineRunner.Notify(GetApiUrl).Before<KnownStages.IUriMatching>();
		}

		public PipelineContinuation GetApiUrl(ICommunicationContext context)
		{
			var uri = context.ApplicationBaseUri +"/";
			var uriSegments = new Uri(uri).Segments;			
			var segments = context.Request.Uri.Segments;
			var rem = segments.Except(uriSegments);

			string apiUrl = String.Join("", rem);

			if (string.IsNullOrEmpty(apiUrl))
				return PipelineContinuation.Continue;

			// may need to retirieve from ApiMethod table here

			// add to PipelineData
			context.PipelineData.Add("ApiUrl", apiUrl);

			return PipelineContinuation.Continue;
		}
	}
}