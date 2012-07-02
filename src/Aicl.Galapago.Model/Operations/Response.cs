using System;
using System.Collections.Generic;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Aicl.Galapago.Model.Operations
{
	public class Response<T>:IHasResponseStatus where T:new()
	{
		public Response ()
		{
			ResponseStatus= new ResponseStatus();
			Data= new List<T>();
		}
		
		public ResponseStatus ResponseStatus { get; set; }
		
		public List<T> Data {get; set;}
		
	}
}