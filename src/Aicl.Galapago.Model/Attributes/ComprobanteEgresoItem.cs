using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/ComprobanteEgresoItem/create","post")]
	[RestService("/ComprobanteEgresoItem/read","get")]
	[RestService("/ComprobanteEgresoItem/read/{Id}","get")]
	[RestService("/ComprobanteEgresoItem/update/{Id}","put")]
	[RestService("/ComprobanteEgresoItem/destroy/{Id}","delete")]
	public partial class ComprobanteEgresoItem
	{
	}
}