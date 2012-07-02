using System;
using System.ComponentModel.DataAnnotations;
using ServiceStack.Common;
using ServiceStack.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace Aicl.Galapago.Model.Types
{
	[Alias("INGRESO")]
	public partial class Ingreso:IHasId<System.Int32>{

		public Ingreso(){}

		[Alias("ID")]
		[Sequence("INGRESO_ID_GEN")]
		[PrimaryKey]
		[AutoIncrement]
		public System.Int32 Id { get; set;} 

		[Alias("NUMERO")]
		public System.Int32 Numero { get; set;} 

		[Alias("ID_TERCERO")]
		public System.Int32 IdTercero { get; set;} 

		[Alias("DESCRIPCION")]
		[StringLength(50)]
		public System.String Descripcion { get; set;} 

		[Alias("FECHA")]
		public System.DateTime Fecha { get; set;} 

		[Alias("PERIODO")]
		[Required]
		[StringLength(6)]
		public System.String Periodo { get; set;} 

		[Alias("VALOR_TOTAL")]
		[DecimalLength(15,2)]
		public System.Decimal ValorTotal { get; set;} 

		[Alias("IVA_TOTAL")]
		[DecimalLength(15,2)]
		public System.Decimal IvaTotal { get; set;} 

		[Alias("PAGADO_CONTADO")]
		[DecimalLength(15,2)]
		public System.Decimal PagadoContado { get; set;} 

		[Alias("DIAS_CREDITO")]
		public System.Int16 DiasCredito { get; set;} 

		[Alias("FECHA_ASENTADO")]
		public System.DateTime? FechaAsentado { get; set;} 

		[Alias("ID_SUCURSAL")]
		public System.Int32 IdSucursal { get; set;} 

		[Alias("ID_CENTRO")]
		public System.Int32 IdCentro { get; set;} 

		[Alias("FECHA_ANULADO")]
		public System.DateTime? FechaAnulado { get; set;} 

		[Alias("EXTERNO")]
		public System.Int16? Externo { get; set;} 

	}
}
