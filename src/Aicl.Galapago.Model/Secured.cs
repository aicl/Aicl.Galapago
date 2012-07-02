using System;
using ServiceStack.ServiceInterface.ServiceModel;
namespace Aicl.Galapago.Model.Types
{
	public class Secured
	{
		public string Name { get; set; }
	}

	public class SecuredResponse:IHasResponseStatus
	{
		public string Result { get; set; }

		public ResponseStatus ResponseStatus { get; set; }
	}
}

