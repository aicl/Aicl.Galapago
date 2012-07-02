using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Userauth/create","post")]
	[RestService("/Userauth/read","get")]
	[RestService("/Userauth/read/{Id}","get")]
	[RestService("/Userauth/update/{Id}","put")]
	[RestService("/Userauth/destroy/{Id}","delete")]
	public partial class Userauth
	{
	}
}