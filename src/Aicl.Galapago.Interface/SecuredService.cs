using System;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Service;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.Text;
using Aicl.Galapago.Model.Types;

namespace Aicl.Galapago.Interface
{
	[Authenticate]
	public class SecuredService : ServiceBase<Secured>
	{
		protected override object Run(Secured request)
		{
			return new SecuredResponse { Result = request.Name };
		}
	}
}

