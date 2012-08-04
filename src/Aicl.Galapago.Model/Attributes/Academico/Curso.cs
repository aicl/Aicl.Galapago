using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Curso/create","post")]
	[RestService("/Curso/read","get")]
	[RestService("/Curso/update/{Id}","put")]
	[RestService("/Curso/destroy/{Id}","delete")]
	public partial class Curso
	{
	}
}