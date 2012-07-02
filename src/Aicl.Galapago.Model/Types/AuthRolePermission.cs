using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("AUTH_ROLE_PERMISSION")]
	public partial class AuthRolePermission:IHasId<System.Int32>{

		public AuthRolePermission(){}

		[Alias("ID")]
		[Sequence("AUTH_ROLE_PERMISSION_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_AUTH_ROLE")]
		public System.Int32 IdAuthRole { get; set;} 

		[Alias("ID_AUTH_PERMISSION")]
		public System.Int32 IdAuthPermission { get; set;} 

	}
}
