using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.BusinessLogic
{
    public static partial class BL
    {
        #region get
        public static InfanteInfoResponse Get(this InfanteInfo request, Factory factory, IHttpRequest httpRequest)
        {
            return factory.Execute(proxy=>{

                return new InfanteInfoResponse{

                };
            });
        }
        #endregion get
    }
}

