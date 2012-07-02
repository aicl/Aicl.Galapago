using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MATRICULA")]
	public partial class Matricula:IHasId<System.Int32>{

		public Matricula(){}

		[Alias("ID")]
		[Sequence("MATRICULA_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("ID_INFANTE")]
		public System.Int32 IdInfante { get; set;} 

		[Alias("ID_ANIO_LECTIVO")]
		public System.Int32 IdAnioLectivo { get; set;} 

		[Alias("ID_CLASE")]
		public System.Int32 IdClase { get; set;} 

		[Alias("FECHA_ASENTADO")]
		public System.DateTime? FechaAsentado { get; set;} 

		[Alias("VALOR_TOTAL")]
		[DecimalLength(15,2)]
		public System.Decimal ValorTotal { get; set;} 

		[Alias("VALOR_PAGADO")]
		[DecimalLength(15,2)]
		public System.Decimal ValorPagado { get; set;} 

	}
}
