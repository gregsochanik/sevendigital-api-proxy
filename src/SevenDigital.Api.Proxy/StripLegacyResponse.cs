using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using OpenRasta.Pipeline;
using OpenRasta.Web;
using SevenDigital.Api.Schema;

namespace SevenDigital.Api.Proxy
{
	public class StripLegacyResponse : IPipelineContributor
	{
		private readonly ITypeGenerator _typeGenerator;

		public StripLegacyResponse(ITypeGenerator typeGenerator)
		{
			_typeGenerator = typeGenerator;
		}

		public void Initialize(IPipeline pipelineRunner)
		{
			pipelineRunner.Notify(StripResponseElement).Before<KnownStages.IUriMatching>();
		}

		public PipelineContinuation StripResponseElement(ICommunicationContext context)
		{
			var passedApiUrl = (string)context.PipelineData["ApiUrl"];
			if (string.IsNullOrEmpty(passedApiUrl))
				return PipelineContinuation.Continue;

			var xmlDocument = (XmlDocument)context.PipelineData["ApiXmlResponse"];

			// read and remove response header to get result
			var responseElement = xmlDocument.SelectSingleNode("/response");
			var responseStatusAttribute = responseElement.Attributes["status"];
			
			var innerXml = responseElement.InnerXml;

			var stringReader = new StringReader(innerXml);
			
			// get the type 
			Type generatedType = responseStatusAttribute.InnerText == "ok" 
			                     	? _typeGenerator.GenerateType(passedApiUrl)
			                     	: typeof(Error);
			
			var serializer = new XmlSerializer(generatedType);
			object deserialized = serializer.Deserialize(stringReader);

			if (responseStatusAttribute.InnerText == "ok")
				context.OperationResult = new OperationResult.OK(deserialized);
			else
				context.OperationResult = new OperationResult.BadRequest { ResponseResource = deserialized };
			return PipelineContinuation.Continue;
		}
	}
}