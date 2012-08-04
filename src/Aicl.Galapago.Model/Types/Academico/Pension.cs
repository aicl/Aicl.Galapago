using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;
using System.ComponentModel.DataAnnotations;

namespace Aicl.Galapago.Model.Types
{
	[Alias("PENSION")]
	public partial class Pension:IHasId<Int32>{

		public Pension(){}

		[Alias("ID")]
		[Sequence("PENSION_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_MATRICULA")]
		public Int32 IdMatricula { get; set;} 

		[Alias("ID_INGRESO")]
		public Int32? IdIngreso { get; set;} 

		[Alias("PERIODO")]
		[StringLength(6)]
		[Required]
		public String Periodo { get; set;} 
	}
}
