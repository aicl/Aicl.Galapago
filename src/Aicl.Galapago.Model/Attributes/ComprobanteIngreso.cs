using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/ComprobanteIngreso/create","post")]
	[RestService("/ComprobanteIngreso/read","get")]
	[RestService("/ComprobanteIngreso/read/{Id}","get")]
	[RestService("/ComprobanteIngreso/update/{Id}","put")]
    [RestService("/ComprobanteIngreso/patch/{Id}/asentar","patch")]
    [RestService("/ComprobanteIngreso/patch/{Id}/reversar","patch")]
    [RestService("/ComprobanteIngreso/patch/{Id}/anular","patch")]
	//[RestService("/ComprobanteIngreso/destroy/{Id}","delete")]
	public partial class ComprobanteIngreso
	{
	}
}
