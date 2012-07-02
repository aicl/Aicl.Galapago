using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/MensualidadProducto/create","post")]
	[RestService("/MensualidadProducto/read","get")]
	[RestService("/MensualidadProducto/read/{Id}","get")]
	[RestService("/MensualidadProducto/update/{Id}","put")]
	[RestService("/MensualidadProducto/destroy/{Id}","delete")]
	public partial class MensualidadProducto
	{
	}
}