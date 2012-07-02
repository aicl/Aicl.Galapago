using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/ComprobanteEgreso/create","post")]
	[RestService("/ComprobanteEgreso/read","get")]
	[RestService("/ComprobanteEgreso/read/{Id}","get")]
	[RestService("/ComprobanteEgreso/update/{Id}","put")]
	[RestService("/ComprobanteEgreso/destroy/{Id}","delete")]
	public partial class ReciboCaja
	{
	}
}