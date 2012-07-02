using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/UsuarioSucursalCentro/create","post")]
	[RestService("/UsuarioSucursalCentro/read","get")]
	[RestService("/UsuarioSucursalCentro/read/{Id}","get")]
	[RestService("/UsuarioSucursalCentro/update/{Id}","put")]
	[RestService("/UsuarioSucursalCentro/destroy/{Id}","delete")]
	public partial class UsuarioSucursalCentro
	{
	}
}