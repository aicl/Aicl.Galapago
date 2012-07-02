using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
    [RestService("/ComprobanteEgresoRetencion/create","post")]
    [RestService("/ComprobanteEgresoRetencion/read","get")]
    [RestService("/ComprobanteEgresoRetencion/read/{Id}","get")]
    //[RestService("/ComprobanteEgresoRetencion/update/{Id}","put")]
    [RestService("/ComprobanteEgresoRetencion/destroy/{Id}","delete")]
    public partial class ComprobanteEgresoRetencion
    {
    }
}