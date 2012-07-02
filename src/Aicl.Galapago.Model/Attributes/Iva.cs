using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Iva/create","post")]
	[RestService("/Iva/read","get")]
	[RestService("/Iva/read/{Id}","get")]
	[RestService("/Iva/update/{Id}","put")]
	[RestService("/Iva/destroy/{Id}","delete")]
	public partial class Iva
	{
	}
}