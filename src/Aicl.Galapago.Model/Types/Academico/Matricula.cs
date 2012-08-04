using System;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("MATRICULA")]
	public partial class Matricula:IHasId<Int32>{

		public Matricula(){}

		[Alias("ID")]
		[Sequence("MATRICULA_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public Int32 Id { get; set;} 

		[Alias("ID_INFANTE")]
		public Int32 IdInfante { get; set;} 

		[Alias("ID_ANIO_LECTIVO")]
		public Int32 IdAnioLectivo { get; set;} 

		[Alias("ID_CLASE")]
		public Int32 IdClase { get; set;} 

		[Alias("FECHA_ASENTADO")]
		public DateTime? FechaAsentado { get; set;} 

		[Alias("VALOR_TOTAL")]
		[DecimalLength(15,2)]
		public Decimal ValorTotal { get; set;} 

		[Alias("VALOR_PAGADO")]
		[DecimalLength(15,2)]
		public Decimal ValorPagado { get; set;} 

	}
}
