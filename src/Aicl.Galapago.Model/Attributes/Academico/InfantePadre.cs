using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/InfantePadre/create","post")]
	[RestService("/InfantePadre/read/{IdInfante}","get")]
	[RestService("/InfantePadre/update/{Id}","put")]
	[RestService("/InfantePadre/destroy/{Id}","delete")]
	public partial class InfantePadre
	{
	}
}