using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Consecutivo/create","post")]
	[RestService("/Consecutivo/read","get")]
	[RestService("/Consecutivo/read/{Id}","get")]
	[RestService("/Consecutivo/update/{Id}","put")]
	[RestService("/Consecutivo/destroy/{Id}","delete")]
	public partial class Consecutivo
	{
	}
}