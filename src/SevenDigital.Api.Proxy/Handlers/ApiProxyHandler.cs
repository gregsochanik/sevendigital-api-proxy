using OpenRasta.Web;

namespace SevenDigital.Api.Proxy.Handlers
{
	public class ApiProxyHandler
	{
		private readonly ICommunicationContext _context;

		public ApiProxyHandler(ICommunicationContext context)
		{
			_context = context;
		}

		public OperationResult Get()
		{
			return _context.OperationResult;
		}

		public OperationResult Post()
		{
			return _context.OperationResult;
		}

		public OperationResult Delete()
		{
			return _context.OperationResult;
		}

		public OperationResult Put()
		{
			return _context.OperationResult;
		}

		public OperationResult Head()
		{
			return _context.OperationResult;
		}
	}
}