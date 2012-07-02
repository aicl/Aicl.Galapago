using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("AUTH_ROLE")]
	public partial class AuthRole:IHasId<System.Int32>{

		public AuthRole(){}

		[Alias("ID")]
		[Sequence("AUTH_ROLE_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("NAME")]
		[Required]
		[StringLength(30)]
		public System.String Name { get; set;} 

		[Alias("DIRECTORY")]
		[StringLength(15)]
		public System.String Directory { get; set;} 
				
		[Alias("SHOW_ORDER")]
		[StringLength(2)]
		public System.String ShowOrder { get; set;} 

		[Alias("TITLE")]
		[Required]
		[StringLength(30)]
		public System.String Title { get; set;} 

	}
}