using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Centro/create","post")]
	[RestService("/Centro/read","get")]
	[RestService("/Centro/read/{Id}","get")]
	[RestService("/Centro/update/{Id}","put")]
	[RestService("/Centro/destroy/{Id}","delete")]
	public partial class Centro
	{
	}
}