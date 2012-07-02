using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/PeriodoSucursal/create","post")]
	[RestService("/PeriodoSucursal/read","get")]
	[RestService("/PeriodoSucursal/read/{Id}","get")]
	[RestService("/PeriodoSucursal/update/{Id}","put")]
	[RestService("/PeriodoSucursal/destroy/{Id}","delete")]
	public partial class PeriodoSucursal
	{
	}
}