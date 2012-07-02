using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/MatriculaProducto/create","post")]
	[RestService("/MatriculaProducto/read","get")]
	[RestService("/MatriculaProducto/read/{Id}","get")]
	[RestService("/MatriculaProducto/update/{Id}","put")]
	[RestService("/MatriculaProducto/destroy/{Id}","delete")]
	public partial class MatriculaProducto
	{
	}
}