using System;
using ServiceStack.ServiceHost;

namespace Aicl.Galapago.Model.Types
{
    [RestService("/UsuarioGiradora/create","post")]
    [RestService("/UsuarioGiradora/read","get")]
    [RestService("/UsuarioGiradora/read/{Id}","get")]
    [RestService("/UsuarioGiradora/update/{Id}","put")]
    [RestService("/UsuarioGiradora/destroy/{Id}","delete")]
    public partial class UsuarioGiradora
    {
    }
}