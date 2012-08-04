using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
	[RestService("/Pension/create","post")]
	[RestService("/Pension/read","get")]
	[RestService("/Pension/update/{Id}","put")]
	[RestService("/Pension/destroy/{Id}","delete")]
	public partial class Pension
	{
	}
}