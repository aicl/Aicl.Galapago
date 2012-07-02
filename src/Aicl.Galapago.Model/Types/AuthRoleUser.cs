using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("AUTH_ROLE_USER")]
	public partial class AuthRoleUser:IHasId<System.Int32>, IHasIdUsuario{

		public AuthRoleUser(){}

		[Alias("ID")]
		[Sequence("AUTH_ROLE_USER_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_AUTH_ROLE")]
		public System.Int32 IdAuthRole { get; set;} 

		[Alias("ID_USERAUTH")]
		public System.Int32 IdUsuario { get; set;} 

	}
}
