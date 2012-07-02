using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/AnioLectivo/create","post")]
	[RestService("/AnioLectivo/read","get")]
	[RestService("/AnioLectivo/read/{Id}","get")]
	[RestService("/AnioLectivo/update/{Id}","put")]
	[RestService("/AnioLectivo/destroy/{Id}","delete")]
	public partial class AnioLectivo
	{
	}
}