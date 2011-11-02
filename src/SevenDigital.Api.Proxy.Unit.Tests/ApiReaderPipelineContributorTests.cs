using System;
using NUnit.Framework;
using OpenRasta.Pipeline;
using OpenRasta.Web;
using Rhino.Mocks;

namespace SevenDigital.Api.Proxy.Unit.Tests
{
	[TestFixture]
	public class ApiReaderPipelineContributorTests
	{
		[Test]
		public void Should_return_correct_path_from_web_path_and_pass_it_into_the_pipeline()
		{
			var mockContext = MockRepository.GenerateStub<ICommunicationContext>();
			var mockRequest = MockRepository.GenerateStub<IRequest>();
			mockContext.Stub(x => x.ApplicationBaseUri).Return(new Uri("http://api.7digital.com/2.0/"));
			mockRequest.Uri = new Uri("http://api.7digital.com/2.0/my/test/uri");
			mockContext.Stub(x => x.Request).Return(mockRequest);
			mockContext.Stub(x => x.PipelineData).Return(new PipelineData());

			var apiReaderPipelineContributor = new ApiUrlPipelineContributor();
			PipelineContinuation routeApiRequest = apiReaderPipelineContributor.GetApiUrl(mockContext);

			Assert.That(routeApiRequest, Is.EqualTo(PipelineContinuation.Continue));
			Assert.That(mockContext.PipelineData["ApiUrl"], Is.EqualTo("my/test/uri"));
		}
	}
}
