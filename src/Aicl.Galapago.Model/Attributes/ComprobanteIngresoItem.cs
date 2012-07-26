using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/ComprobanteIngresoItem/create","post")]
	[RestService("/ComprobanteIngresoItem/read","get")]
	[RestService("/ComprobanteIngresoItem/read/{Id}","get")]
	[RestService("/ComprobanteIngresoItem/update/{Id}","put")]
	[RestService("/ComprobanteIngresoItem/destroy/{Id}","delete")]
	public partial class ComprobanteIngresoItem
	{
	}
}