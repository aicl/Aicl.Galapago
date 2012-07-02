using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/MatriculaPago/create","post")]
	[RestService("/MatriculaPago/read","get")]
	[RestService("/MatriculaPago/read/{Id}","get")]
	[RestService("/MatriculaPago/update/{Id}","put")]
	[RestService("/MatriculaPago/destroy/{Id}","delete")]
	public partial class MatriculaPago
	{
	}
}