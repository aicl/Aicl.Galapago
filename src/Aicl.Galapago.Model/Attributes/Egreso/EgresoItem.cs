using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/EgresoItem/create","post")]
	[RestService("/EgresoItem/read","get")]
	[RestService("/EgresoItem/read/{IdEgreso}","get")]
	[RestService("/EgresoItem/update/{Id}","put")]
	[RestService("/EgresoItem/destroy/{Id}","delete")]
	public partial class EgresoItem
	{
	}
}