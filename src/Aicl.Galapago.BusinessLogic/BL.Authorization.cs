using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceHost;
using Mono.Linq.Expressions;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;

using Aicl.Galapago.DataAccess;
namespace Aicl.Galapago.BusinessLogic
{
	public static partial class BL
	{
				
		public static AuthorizationResponse GetAuthorizations(this Authorization request, 
		                                           Factory factory, IRequestContext requestContext){
			
			var httpRequest = requestContext.Get<IHttpRequest>();	
			IAuthSession session = httpRequest.GetSession();
						
			if (!session.HasRole(RoleNames.Admin))
			{
				request.UserId= int.Parse(session.UserAuthId);
			}
			
			List<AuthRole> roles = new List<AuthRole>();
			List<string> permissions= new List<string>();
			
            List<AuthRoleUser> aur= new List<AuthRoleUser>();
            List<AuthRole> rol = new List<AuthRole>();
            List<AuthPermission> per = new List<AuthPermission>();
            List<AuthRolePermission> rol_per = new List<AuthRolePermission>();
            List<UsuarioSucursalCentro> usc= new List<UsuarioSucursalCentro>();
            List<Sucursal> sucursales = new List<Sucursal>();
            List<Centro> centros = new List<Centro>(); 

            List<SucursalAutorizada> sa = new List<SucursalAutorizada>();
            List<CentroAutorizado> ca = new List<CentroAutorizado>();

            List<CodigoDocumento> cd = new List<CodigoDocumento>();
            List<Rubro> rubros = new List<Rubro>();
			List<Ciudad> ciudades = new List<Ciudad>();
			List<TipoDocumento> tipos = new List<TipoDocumento>();

            factory.Execute(proxy=>
            {
                aur= proxy.GetByIdUsuarioFromCache<AuthRoleUser>(request.UserId);
                rol= proxy.GetFromCache<AuthRole>();
                per= proxy.GetFromCache<AuthPermission>();
                rol_per= proxy.GetFromCache<AuthRolePermission>();
                usc= proxy.GetByIdUsuarioFromCache<UsuarioSucursalCentro>(request.UserId);
                sucursales= proxy.GetFromCache<Sucursal>();
                centros= proxy.GetFromCache<Centro>();
                cd= proxy.Get<CodigoDocumento>();

                foreach( var r in aur)
                {
                    AuthRole ar= rol.First(x=>x.Id== r.IdAuthRole);
                    roles.Add(ar);
                    rol_per.Where(q=>q.IdAuthRole==ar.Id).ToList().ForEach(y=>{
                        AuthPermission up=  per.First( p=> p.Id== y.IdAuthPermission);
                        if( permissions.IndexOf(up.Name) <0)
                            permissions.Add(up.Name);
                    }) ;
                };
                                    
                foreach(UsuarioSucursalCentro item in usc){
                    
                    Sucursal s= sucursales.FirstOrDefault(r=> r.Id== item.IdSucursal && r.Activo);
                    
                    if(s!=default(Sucursal) && sa.FirstOrDefault(x=>x.Id==s.Id)== default(SucursalAutorizada))
                        sa.Add(new SucursalAutorizada(){Id=s.Id, Codigo=s.Codigo, Nombre=s.Nombre});
                    
                    Centro c= centros.FirstOrDefault(r=> r.Id== item.IdCentro && r.Activo);
                    
                    if(c!=default(Centro))
                        ca.Add(new CentroAutorizado(){Id=c.Id, Codigo=c.Codigo, Nombre=c.Nombre, IdSucursal=item.IdSucursal});
                }

                var predicate = PredicateBuilder.False<Rubro>();
                foreach( var rca in ca){
                    int c= rca.Id;
                    int s = rca.IdSucursal;
                    predicate= predicate.OrElse(r=>r.IdSucursal==s && r.IdCentro==c);
                }

                Expression<Func<Rubro, bool>> predicate2= r=>r.Activo && r.PresupuestoActivo && r.Codigo.Contains(".");
                predicate= predicate.AndAlso(predicate2);

                rubros= proxy.Get<Rubro>(predicate);

				ciudades = proxy.Get<Ciudad>();
				tipos = proxy.Get<TipoDocumento>();

            });
			
			return new AuthorizationResponse(){
				Permissions= permissions,
				Roles= roles,
				Sucursales= sa,
				Centros= ca,
                CodigosDocumento=cd,
                Rubros=rubros,
				Ciudades=ciudades,
				TiposDocumento= tipos
			};
		}			
	}
}