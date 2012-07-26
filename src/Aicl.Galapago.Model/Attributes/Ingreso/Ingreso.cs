using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Ingreso/create","post")]
	[RestService("/Ingreso/read","get")]
	[RestService("/Ingreso/read/{Id}","get")]
	[RestService("/Ingreso/update/{Id}","put")]
	[RestService("/Ingreso/patch/{Id}/asentar","patch")]
    [RestService("/Ingreso/patch/{Id}/reversar","patch")]
    [RestService("/Ingreso/patch/{Id}/anular","patch")]
	//[RestService("/Ingreso/destroy/{Id}","delete")]
	public partial class Ingreso
	{
	}
}