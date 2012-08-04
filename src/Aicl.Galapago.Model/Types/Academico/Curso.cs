using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("CURSO")]
	public partial class Curso:IHasId<Int32>{

		public Curso(){}

		[Alias("ID")]
		[Sequence("CURSO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("DESCRIPCION")]
		[Required]
		[StringLength(30)]
		public String Descripcion { get; set;} 

		[Alias("CALENDARIO")]
		[Required]
		[StringLength(1)]
		public String Nombre { get; set;} 

		[Alias("FECHA_INCIO")]
		public DateTime FechaIncio { get; set;} 

		[Alias("FECHA_TERMINACION")]
		public DateTime FechaTerminacion { get; set;} 

	}
}
