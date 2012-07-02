using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/ComprobanteEgreso/create","post")]
	[RestService("/ComprobanteEgreso/read","get")]
	[RestService("/ComprobanteEgreso/read/{Id}","get")]
	[RestService("/ComprobanteEgreso/update/{Id}","put")]
    [RestService("/ComprobanteEgreso/patch/{Id}/asentar","patch")]
    [RestService("/ComprobanteEgreso/patch/{Id}/reversar","patch")]
    [RestService("/ComprobanteEgreso/patch/{Id}/anular","patch")]
	[RestService("/ComprobanteEgreso/destroy/{Id}","delete")]
	public partial class ComprobanteEgreso
	{
	}
}