using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
    [RestService("/InfanteAcudiente/create","post")]
    [RestService("/InfanteAcudiente/read/{IdInfante}","get")]
    [RestService("/InfanteAcudiente/update/{Id}","put")]
    [RestService("/InfanteAcudiente/destroy/{Id}","delete")]
    public partial class InfanteAcudiente
    {
    }
}
