using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Periodo/create","post")]
	[RestService("/Periodo/read","get")]
	[RestService("/Periodo/read/{Id}","get")]
	[RestService("/Periodo/update/{Id}","put")]
	[RestService("/Periodo/destroy/{Id}","delete")]
	public partial class Periodo
	{
	}
}