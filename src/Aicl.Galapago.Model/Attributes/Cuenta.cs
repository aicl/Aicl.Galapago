using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Cuenta/create","post")]
	[RestService("/Cuenta/read","get")]
	[RestService("/Cuenta/read/{Id}","get")]
	[RestService("/Cuenta/update/{Id}","put")]
	[RestService("/Cuenta/destroy/{Id}","delete")]
	public partial class Cuenta
	{
	}
}