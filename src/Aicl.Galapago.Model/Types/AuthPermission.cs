using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("AUTH_PERMISSION")]
	public partial class AuthPermission:IHasId<System.Int32>{

		public AuthPermission(){}

		[Alias("ID")]
		[Sequence("AUTH_PERMISSION_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("NAME")]
		[Required]
		[StringLength(30)]
		public System.String Name { get; set;} 

	}
}
