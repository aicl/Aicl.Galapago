using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Sucursal/create","post")]
	[RestService("/Sucursal/read","get")]
	[RestService("/Sucursal/read/{Id}","get")]
	[RestService("/Sucursal/update/{Id}","put")]
	[RestService("/Sucursal/destroy/{Id}","delete")]
	public partial class Sucursal
	{
	}
}