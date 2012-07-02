using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/ReciboCajaAct/create","post")]
	[RestService("/ReciboCajaAct/read","get")]
	[RestService("/ReciboCajaAct/read/{Id}","get")]
	[RestService("/ReciboCajaAct/update/{Id}","put")]
	[RestService("/ReciboCajaAct/destroy/{Id}","delete")]
	public partial class ReciboCajaAct
	{
	}
}
