using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Ciudad/create","post")]
	[RestService("/Ciudad/read","get")]
	[RestService("/Ciudad/update/{Id}","put")]
	[RestService("/Ciudad/destroy/{Id}","delete")]
	public partial class Ciudad
	{
	}
}