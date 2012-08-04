using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Infante/create","post")]
	[RestService("/Infante/read","get")]
	[RestService("/Infante/read/{Id}","get")]
	[RestService("/Infante/update/{Id}","put")]
	[RestService("/Infante/destroy/{Id}","delete")]
	public partial class Infante
	{
	}
}