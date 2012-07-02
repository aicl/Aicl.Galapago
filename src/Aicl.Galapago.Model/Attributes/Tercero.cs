using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Tercero/create","post")]
	[RestService("/Tercero/read","get")]
	[RestService("/Tercero/read/{Id}","get")]
	[RestService("/Tercero/update/{Id}","put")]
	[RestService("/Tercero/destroy/{Id}","delete")]
	public partial class Tercero
	{
	}
}