using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/MatriculaItem/create","post")]
	[RestService("/MatriculaItem/read","get")]
	[RestService("/MatriculaItem/update/{Id}","put")]
	[RestService("/MatriculaItem/destroy/{Id}","delete")]
	public partial class MatriculaItem
	{
	}
}