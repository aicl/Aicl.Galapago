using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Egreso/create","post")]
	[RestService("/Egreso/read","get")]
	[RestService("/Egreso/read/{Id}","get")]
	[RestService("/Egreso/update/{Id}","put")]
    [RestService("/Egreso/patch/{Id}/asentar","patch")]
    [RestService("/Egreso/patch/{Id}/reversar","patch")]
    [RestService("/Egreso/patch/{Id}/anular","patch")]
	[RestService("/Egreso/destroy/{Id}","delete")]
	public partial class Egreso
	{
	}
}