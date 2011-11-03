using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using OpenRasta.DI;
using OpenRasta.DI.Windsor;
using OpenRasta.Pipeline;
using SevenDigital.Api.Proxy.PipelineContributors;
using SevenDigital.Api.Schema;

namespace SevenDigital.Api.Proxy.IOC
{
	public class DependencyResolver : IDependencyResolverAccessor
	{
		private static IWindsorContainer _container;

		public static IWindsorContainer Container
		{
			get { return _container ?? (_container = ConfigureContainer()); }
		}

		public IDependencyResolver Resolver
		{
			get { return new WindsorDependencyResolver(Container); }
		}

		static IWindsorContainer ConfigureContainer()
		{
			_container = new WindsorContainer(new XmlInterpreter());
			_container.Register(
					Component.For<ITypeGenerator>().ImplementedBy<ApiEndpointTypeGenerator>(),
					Component.For<IPipelineContributor>().ImplementedBy<FindApiEndpointUri>(),
					Component.For<IPipelineContributor>().ImplementedBy<ApiRouter>(),
					Component.For<IPipelineContributor>().ImplementedBy<StripLegacyResponse>()
				);
			return _container;
		}
	}
}
