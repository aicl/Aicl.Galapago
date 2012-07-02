using System;
using ServiceStack.ServiceHost;
namespace Aicl.Galapago.Model.Types
{
	[RestService("/CodigoDocumento/create","post")]
	[RestService("/CodigoDocumento/read","get")]
	[RestService("/CodigoDocumento/read/{Id}","get")]
	[RestService("/CodigoDocumento/update/{Id}","put")]
	[RestService("/CodigoDocumento/destroy/{Id}","delete")]
	public partial class CodigoDocumento
	{
		
	}
}

