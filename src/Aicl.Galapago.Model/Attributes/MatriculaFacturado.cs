using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/MatriculaFacturado/create","post")]
	[RestService("/MatriculaFacturado/read","get")]
	[RestService("/MatriculaFacturado/read/{Id}","get")]
	[RestService("/MatriculaFacturado/update/{Id}","put")]
	[RestService("/MatriculaFacturado/destroy/{Id}","delete")]
	public partial class MatriculaFacturado
	{
	}
}