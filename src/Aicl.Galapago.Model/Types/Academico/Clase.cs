using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CLASE")]
	public partial class Clase:IHasId<Int32>{

		public Clase(){}

		[Alias("ID")]
		[Sequence("CLASE_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("DESCRIPCION")]
		[Required]
		[StringLength(30)]
		public String Descripcion { get; set;} 

		[Alias("ACTIVO")]
		public bool Activo { get; set;} 

	}
}
