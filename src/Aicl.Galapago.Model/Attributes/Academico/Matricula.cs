using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Matricula/create","post")]
	[RestService("/Matricula/read","get")]
	[RestService("/Matricula/update/{Id}","put")]
	[RestService("/Matricula/destroy/{Id}","delete")]
	public partial class Matricula
	{
	}
}