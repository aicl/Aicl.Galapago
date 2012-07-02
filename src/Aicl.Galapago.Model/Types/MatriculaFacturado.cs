using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MATRICULA_FACTURADO")]
	public partial class MatriculaFacturado:IHasId<System.Int32>{

		public MatriculaFacturado(){}

		[Alias("ID")]
		[Sequence("MATRICULA_FACTURADO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_MATRICULA")]
		public System.Int32 IdMatricula { get; set;} 

		[Alias("PERIODO")]
		[Required]
		[StringLength(6)]
		public System.String Periodo { get; set;} 

		[Alias("ID_INGRESO")]
		public System.Int32 IdIngreso { get; set;} 

	}
}
