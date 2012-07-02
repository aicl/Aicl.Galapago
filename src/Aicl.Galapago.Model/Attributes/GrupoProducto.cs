using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/GrupoProducto/create","post")]
	[RestService("/GrupoProducto/read","get")]
	[RestService("/GrupoProducto/read/{Id}","get")]
	[RestService("/GrupoProducto/update/{Id}","put")]
	[RestService("/GrupoProducto/destroy/{Id}","delete")]
	public partial class GrupoProducto
	{
	}
}