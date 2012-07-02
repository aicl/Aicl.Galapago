using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/MayorContabilidad2012/create","post")]
	[RestService("/MayorContabilidad2012/read","get")]
	[RestService("/MayorContabilidad2012/read/{Id}","get")]
	[RestService("/MayorContabilidad2012/update/{Id}","put")]
	[RestService("/MayorContabilidad2012/destroy/{Id}","delete")]
	public partial class MayorContabilidad2012
	{
	}
}