using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/AsientoItem/create","post")]
	[RestService("/AsientoItem/read","get")]
	[RestService("/AsientoItem/read/{Id}","get")]
	[RestService("/AsientoItem/update/{Id}","put")]
	[RestService("/AsientoItem/destroy/{Id}","delete")]
	public partial class AsientoItem
	{
	}
}