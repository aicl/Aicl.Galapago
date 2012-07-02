using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Clase/create","post")]
	[RestService("/Clase/read","get")]
	[RestService("/Clase/read/{Id}","get")]
	[RestService("/Clase/update/{Id}","put")]
	[RestService("/Clase/destroy/{Id}","delete")]
	public partial class Clase
	{
	}
}