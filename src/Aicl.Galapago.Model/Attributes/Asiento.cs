using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Asiento/create","post")]
	[RestService("/Asiento/read","get")]
	[RestService("/Asiento/read/{Id}","get")]
	[RestService("/Asiento/update/{Id}","put")]
	[RestService("/Asiento/destroy/{Id}","delete")]
	[RestService("/Asiento/patch/{Id}","PATCH")]
	public partial class Asiento
	{
	}
}