using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
    [RestService("/ComprobanteIngresoRetencion/create","post")]
    [RestService("/ComprobanteIngresoRetencion/read","get")]
    [RestService("/ComprobanteIngresoRetencion/read/{Id}","get")]
    //[RestService("/ComprobanteIngresoRetencion/update/{Id}","put")]
    [RestService("/ComprobanteIngresoRetencion/destroy/{Id}","delete")]
    public partial class ComprobanteIngresoRetencion
    {
    }
}