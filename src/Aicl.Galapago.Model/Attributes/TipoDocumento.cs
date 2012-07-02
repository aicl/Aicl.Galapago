using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/TipoDocumento/create","post")]
	[RestService("/TipoDocumento/read","get")]
	[RestService("/TipoDocumento/read/{Id}","get")]
	[RestService("/TipoDocumento/update/{Id}","put")]
	[RestService("/TipoDocumento/destroy/{Id}","delete")]
	public partial class TipoDocumento
	{
	}
}