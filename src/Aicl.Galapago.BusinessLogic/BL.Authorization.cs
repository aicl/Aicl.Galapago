using System;
using System.Data;
using System.Text;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.Common.Utils;
using ServiceStack.DesignPatterns.Model;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.CacheAccess;
using ServiceStack.ServiceHost;
using ServiceStack.Redis;
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

            factory.Execute(proxy=>
            {
                aur= DAL.GetByIdUsuarioFromCache<AuthRoleUser>(proxy, request.UserId);
                rol= DAL.GetFromCache<AuthRole>(proxy);
                per= DAL.GetFromCache<AuthPermission >(proxy);
                rol_per= DAL.GetFromCache<AuthRolePermission >(proxy);
                usc= DAL.GetByIdUsuarioFromCache<UsuarioSucursalCentro>(proxy,request.UserId);
                sucursales= DAL.GetFromCache<Sucursal>( proxy);
                centros= DAL.GetFromCache<Centro>(proxy);
                cd= DAL.Get<CodigoDocumento>(proxy);

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

            });
						
			
			
			return new AuthorizationResponse(){
				Permissions= permissions,
				Roles= roles,
				Sucursales= sa,
				Centros= ca,
                CodigosDocumento=cd,
                Rubros=rubros
			};
		}
				
		
	}
	
}