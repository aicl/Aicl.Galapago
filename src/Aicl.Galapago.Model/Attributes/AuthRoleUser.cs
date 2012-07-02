using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/AuthRoleUser/create","post")]
	[RestService("/AuthRoleUser/read","get")]
	[RestService("/AuthRoleUser/read/{Id}","get")]
	[RestService("/AuthRoleUser/update/{Id}","put")]
	[RestService("/AuthRoleUser/destroy/{Id}","delete")]
	public partial class AuthRoleUser
	{
	}
}