using System;
using System.Collections.Generic;
using System.Linq;
﻿using ServiceStack.CacheAccess;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Redis;

using Aicl.Galapago.Model.Types;
using Aicl.Galapago.Model.Operations;
using Aicl.Galapago.DataAccess;
using Aicl.Galapago.BusinessLogic;

namespace Aicl.Galapago.Interface
{
	public class AuthenticationService:RestServiceBase<Authentication>
	{
		public Factory Factory{ get; set;}
		
		public override object OnPost (Authentication request)
		{
			return OnGet(request);
		}
		

		public override object OnGet (Authentication request)
		{

            AuthService authService = ResolveService<AuthService>();
            
            object fr= authService.Post(new Auth {
                provider = AuthService.CredentialsProvider,
                UserName = request.UserName,
                Password = request.Password
            }) ;
                        
            
            IAuthSession session = this.GetSession();
            
            if(!session.IsAuthenticated)
            {
                HttpError e = fr as HttpError;
                if(e!=null) throw e;
                
                Exception ex = fr as Exception;
                throw ex;
            };
            
            Authorization auth = new Authorization(){
                UserId= int.Parse(session.UserAuthId)
            };
            
            AuthorizationResponse aur = auth.GetAuthorizations(Factory,RequestContext);
            
            session.Permissions= aur.Permissions;
            session.Roles= (from r in aur.Roles select r.Name).ToList();
            
            authService.SaveSession(session);
                        
            return new AuthenticationResponse(){
                DisplayName= session.DisplayName.IsNullOrEmpty()? session.UserName: session.DisplayName,
                Roles= aur.Roles,
                Permissions= aur.Permissions,
                Centros= aur.Centros,
                Sucursales=aur.Sucursales
            };

		}
		
		public override object OnDelete (Authentication request)
		{
			AuthService authService = base.ResolveService<AuthService>();
			var response =authService.Delete(new Auth {
					provider = AuthService.LogoutAction
			});
				
			return response;
		}
		
	}
}