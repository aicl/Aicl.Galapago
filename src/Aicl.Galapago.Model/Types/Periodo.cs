using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("PERIODO")]
	public partial class Periodo:IHasId<System.Int32>{

		public Periodo(){}

		[Alias("ID")]
		[Sequence("PERIODO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("PERIODO")]
		[Required]
		[StringLength(6)]
		public System.String Name { get; set;} 

		[Alias("BLOQUEADO")]
		public System.Boolean Bloqueado { get; set;} 

	}
}
