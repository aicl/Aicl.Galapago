using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Departamento/create","post")]
	[RestService("/Departamento/read","get")]
	[RestService("/Departamento/read/{Id}","get")]
	[RestService("/Departamento/update/{Id}","put")]
	[RestService("/Departamento/destroy/{Id}","delete")]
	public partial class Departamento
	{
	}
}