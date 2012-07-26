using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/IngresoItem/create","post")]
	[RestService("/IngresoItem/read","get")]
	[RestService("/IngresoItem/read/{IdIngreso}","get")]
	[RestService("/IngresoItem/update/{Id}","put")]
	[RestService("/IngresoItem/destroy/{Id}","delete")]
	public partial class IngresoItem
	{
	}
}