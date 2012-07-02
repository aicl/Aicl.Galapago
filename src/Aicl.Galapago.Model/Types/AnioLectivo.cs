using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("ANIO_LECTIVO")]
	public partial class AnioLectivo:IHasId<System.Int32>{

		public AnioLectivo(){}

		[Alias("ID")]
		[Sequence("ANIO_LECTIVO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("DESCRIPCION")]
		[Required]
		[StringLength(30)]
		public System.String Descripcion { get; set;} 

		[Alias("CALENDARIO")]
		[StringLength(1)]
		public System.String Calendario { get; set;} 

		[Alias("ANIO_INICIO")]
		[Required]
		[StringLength(4)]
		public System.String AnioInicio { get; set;} 

		[Alias("ANIO_TERMINACION")]
		[Required]
		[StringLength(4)]
		public System.String AnioTerminacion { get; set;} 

		[Alias("ACTIVO")]
		public System.Int16? Activo { get; set;} 

	}
}
