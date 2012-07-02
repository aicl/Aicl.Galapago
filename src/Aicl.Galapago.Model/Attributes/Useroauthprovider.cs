using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Useroauthprovider/create","post")]
	[RestService("/Useroauthprovider/read","get")]
	[RestService("/Useroauthprovider/read/{Id}","get")]
	[RestService("/Useroauthprovider/update/{Id}","put")]
	[RestService("/Useroauthprovider/destroy/{Id}","delete")]
	public partial class Useroauthprovider
	{
	}
}