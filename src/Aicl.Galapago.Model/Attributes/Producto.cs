using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Producto/create","post")]
	[RestService("/Producto/read","get")]
	[RestService("/Producto/read/{Id}","get")]
	[RestService("/Producto/update/{Id}","put")]
	[RestService("/Producto/destroy/{Id}","delete")]
	public partial class Producto
	{
	}
}