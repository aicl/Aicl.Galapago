using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("INFANTE_PADRE")]
	public partial class InfantePadre:IHasId<System.Int32>{

		public InfantePadre(){}

		[Alias("ID")]
		[Sequence("INFANTE_PADRE_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_INFANTE")]
		public System.Int32 IdInfante { get; set;} 

		[Alias("ID_TERCERO")]
		public System.Int32 IdTercero { get; set;} 

		[Alias("PARENTESCO")]
		[Required]
		[StringLength(30)]
		public System.String Parentesco { get; set;} 

	}
}
